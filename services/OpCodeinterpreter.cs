using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnnamedOS.services
{
    public class OpCodeinterpreter
    {
        public static void ExecuteCode(byte[] code)
        {
            for (int i = 0; i < code.Length; i += 2)
            {
                string responds = "";
                if (code[i] == 0x00) responds = ExecuteSystemCall(code, i + 1);




                if (responds.StartsWith("+"))
                {
                    i += int.Parse(responds.Remove(0, 1));
                }
                else if (responds == "EOF")
                {
                    Console.WriteLine("End Of File");
                    break;
                    //return;
                }
            }

            Console.ReadLine();
        }

        private static string ExecuteSystemCall(byte[] code, int pointer)
        {
            byte method = code[pointer];

            if (method == 0x00) return "EOF";
            else if (method == 0x01) return "+" + (code[pointer + 1] * code[pointer + 2]) * 2;
            else if (method == 0x02) { Console.Clear(); return ""; }
            else if (method == 0x03) { Console.WriteLine("test"); return ""; }


            return "";
        }
    }
}
