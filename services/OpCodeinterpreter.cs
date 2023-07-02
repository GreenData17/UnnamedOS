using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private static string ExecuteSystemCall(string[] code, int pointer)
        {
            string method = code[pointer];

            if (method == "00") return "EOF";  // indicates the end of the code
            else if (method == "01") return "+2";  // next byte group
            else if (method == "02") return "DEBUG";  // activates Debug mode


            return "";
        }

        private static string ExecuteTextModeCall(string[] code, int pointer)
        {
            string method = code[pointer];

            if (method == "00")
            {
                int offset = 1;
                string output = "";
                bool isReading = true;

                while (isReading)
                {
                    if (code[pointer + offset] == "03")
                    {
                        isReading = false;
                        break;
                    }
                    else if (code[pointer + offset] == "48")
                    {
                        output += "H";
                    }
                    else if (code[pointer + offset] == "69")
                    {
                        output += "i";
                    }
                    else if (code[pointer + offset] == "7E")
                    {
                        output += "~";
                    }

                    offset += 1;
                }

                TempTextConsole.WriteToConsole(output);
                return "+" + (offset);
            }

            return "";
        }
    }
}
