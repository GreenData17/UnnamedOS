using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnnamedOS.services;
using UnnamedOS.services.Utils;
using UnnamedOS.TempApps;

namespace UnnamedOS.Core
{
    public class Process
    {
        private bool isValid = false;

        private int emptyMemorySpace = 1000;
        private int entryPoint = 0; // starts at 0, 5 is the offset for the file type verification.
        private int loopEntryPoint = 0;
        private int variabelEntryPoint = 6; // indicates where the variable data starts at.
        private List<byte> stack;
        public byte[] memory;

        public Process(byte[] file, byte[] arguments = null)
        {
            if (arguments != null && arguments[0] != 0x00)
            {
                VerifyIsFile(file);
            }

            memory = new byte[file.Length + emptyMemorySpace];
            Array.Copy(file, memory, file.Length);

            variabelEntryPoint = file.Length + 1;

            isValid = true;
        }

        public void Start()
        {
            if(!isValid) { TempTextConsole.WriteToConsole("Error - file can not be run"); return; }

            byte[] code = new byte[memory.Length - emptyMemorySpace - entryPoint];

            Array.Copy(memory, entryPoint, code, 0, code.Length);

            if (!ExecuteCode())
            {
                PanicService.ThrowPanic("Unnamed Process", "Process was Illegally closed!");
            }
        }

        public void Stop()
        {
            isValid = false;
            emptyMemorySpace = 0;
            entryPoint = 0;
            memory = null;
        }

        public void Update()
        {
            if (loopEntryPoint == 0)
            {
                Stop();
            }


        }


        // -- public methods -- //

        public void DumpMemory(int? dumpAmount = null)
        {
            int counter = 0;
            foreach (var s in Converter.ByteArrayToHexArray(memory))
            {
                Console.Write(s + " ");
                counter++;
                if(dumpAmount != null && counter == dumpAmount) break;
            }
        }


        // -- internal methods -- //

        private void VerifyIsFile(byte[] file)
        {
            if (file == null) TempTextConsole.WriteToConsole("Error - no content");

            if (file[0] != 0x2E) TempTextConsole.WriteToConsole("Error - file corrupted");              // .
            if (file[1] != 0x4E) TempTextConsole.WriteToConsole("Error - file is not a executable");    // N
            if (file[2] != 0x4F) TempTextConsole.WriteToConsole("Error - file is not a executable");    // O
            if (file[3] != 0x45) TempTextConsole.WriteToConsole("Error - file is not a executable");    // E
            if (file[4] != 0x00) TempTextConsole.WriteToConsole("Error - file corrupted");              // NUL

            entryPoint = 5;
        }


        // -- Op Code Interpreter -- //

        public bool ExecuteCode(int entryPointOffset = 0)
        {
            bool debugMode = false;

            try
            {
                for (int i = entryPointOffset; i < memory.Length; i += 2)
                {
                    string responds = "";
                    if (memory[i] == 0x00) responds = ExecuteSystemCall(i + 1);
                    else if (memory[i] == 0x04) responds = ExecuteTextModeCall(i + 1);




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
                        if (debugMode)
                            TempTextConsole.WriteToConsole("-- End Of File --");
                        return true;
                    }
                }
            }
            catch
            {
                if (debugMode)
                    TempTextConsole.WriteToConsole("-- Invalid Code! --");

                return false;
            }

            return false;
        }

        // SYSTEM CALLS //

        private string ExecuteSystemCall(int pointer)
        {
            byte method = memory[pointer];

            if (method == 0x00) return "EOF";  // indicates the end of the code
            if (method == 0x01) return "+2";  // next byte group
            if (method == 0x02) return "DEBUG";  // activates Debug mode


            return "";
        }

        // TEXT MODE CALLS //

        private string ExecuteTextModeCall(int pointer)
        {
            byte method = memory[pointer];

            if (method == 0x00) return ConsoleWriteLine(pointer);

            return "";
        }

        private string ConsoleWriteLine( int pointer)
        {
            int offset = 1;
            string output = "";
            bool isReading = true;

            while (isReading)
            {
                string character = Converter.HexToAscii(memory[pointer + offset]);

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

            TempTextConsole.WriteToConsole(output);   // TODO: has to be removed
            return "+" + (offset);
        }
    }
}
