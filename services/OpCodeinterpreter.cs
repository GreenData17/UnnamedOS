﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnnamedOS.services.Utils;
using UnnamedOS.TempApps;

namespace UnnamedOS.services
{
    public class OpCodeinterpreter
    {
        public static void ExecuteCode(string[] code)
        {
            bool debugMode = false;

            try
            {
                for (int i = 0; i < code.Length; i += 2)
                {
                    string responds = "";
                    if (code[i] == "00") responds = ExecuteSystemCall(code, i + 1);
                    else if (code[i] == "04") responds = ExecuteTextModeCall(code, i + 1);




                    if (responds.StartsWith("+"))
                    {
                        i += int.Parse(responds.Remove(0, 1));
                    }
                    else if (responds == "DEBUG")
                    {
                        debugMode = true;
                    }
                    else if (responds == "EOF")
                    {
                        if(debugMode)
                            TempTextConsole.WriteToConsole("-- End Of File --");
                        return;
                    }
                }
            }
            catch
            {
                if(debugMode)
                    TempTextConsole.WriteToConsole("-- Invalid Code! --");
            }
        }

        // SYSTEM CALLS //

        private static string ExecuteSystemCall(string[] code, int pointer)
        {
            string method = code[pointer];

            if (method == "00") return "EOF";  // indicates the end of the code
            else if (method == "01") return "+2";  // next byte group
            else if (method == "02") return "DEBUG";  // activates Debug mode


            return "";
        }

        // TEXT MODE CALLS //

        private static string ExecuteTextModeCall(string[] code, int pointer)
        {
            string method = code[pointer];

            if (method == "00") return ConsoleWriteLine(code, pointer);

            return "";
        }

        private static string ConsoleWriteLine(string[] code, int pointer)
        {
            int offset = 1;
            string output = "";
            bool isReading = true;

            while (isReading)
            {
                string character = Converter.HexToAscii(code[pointer + offset]);

                if (character == "ETX")
                {
                    isReading = false;
                    break;
                }
                else
                {
                    output += character;
                }

                offset += 1;
            }

            TempTextConsole.WriteToConsole(output);
            return "+" + (offset);
        }
    }
}
