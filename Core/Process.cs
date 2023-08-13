using System;
using System.Collections.Generic;
using UnnamedOS.services;
using UnnamedOS.services.Utils;
using UnnamedOS.TempApps;

namespace UnnamedOS.Core
{
    public class Process
    {
        // Process Info
        private string name = "";
        private bool isValid = false;
        private int exitCode = 0;
        //private int AccessLevel = 0;  // TODO: make it so that highest process will cause a panic if failed
        private int emptyMemorySpace = 1000;

        // Entrypoints
        private int entryPoint = 0; // starts at 0, 5 is the offset for the file type verification.
        private int loopEntryPoint = 0;
        private int variabelEntryPoint = 6; // indicates where the variable data starts at.

        // Memory
        private List<byte> stack;
        public byte[] memory;

        public Process(byte[] code, string processName = "unmanagedProcess", byte[] arguments = null)
        {
            name = processName;
            Kernel.instance.processes.Add(this);

            memory = new byte[code.Length + emptyMemorySpace];
            Array.Copy(code, memory, code.Length);

            variabelEntryPoint = code.Length + 1;

            isValid = true;
        }

        public void Start()
        {
            if(!isValid) { TempTextConsole.WriteToConsole("Error - file can not be run"); return; }
            

            exitCode = ExecuteCode();
            if (GetExitCode() != 0)
            {
                Stop();
                PanicService.ThrowPanic(GetName(), "Process was Illegally closed!", $"Process exited with code {GetExitCode()}.");
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

        public string GetName() => name;
        public int GetExitCode() => exitCode;


        // -- Op Code Interpreter -- //

        public int ExecuteCode(int entryPointOffset = 0)
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
                            TempTextConsole.WriteToConsole("-- End Of File --");   // TODO: has to be removed
                        return 0;
                    }else if (responds.StartsWith("ERROR"))
                    {
                        if (debugMode)
                            TempTextConsole.WriteToConsole("-- Critical Error! --");   // TODO: has to be removed

                        return int.Parse(responds.Remove(0, 6));
                    }
                }
            }
            catch
            {
                if (debugMode)
                    TempTextConsole.WriteToConsole("-- Invalid Code! --");   // TODO: has to be removed

                return -1;
            }

            return -1;
        }

        // SYSTEM CALLS //

        private string ExecuteSystemCall(int pointer)
        {
            byte method = memory[pointer];

            if (method == 0x00) return "ERROR 1"; // Invalid System call
            if (method == 0x01) return "EOF";  // indicates the end of the code
            if (method == 0x02) return "+2";  // next byte group
            if (method == 0x03) return "DEBUG";  // activates Debug mode


            return "ERROR 1"; // Invalid System call
        }

        // TEXT MODE CALLS //

        private string ExecuteTextModeCall(int pointer)
        {
            byte method = memory[pointer];

            if (method == 0x00) return ConsoleWriteLine(pointer);

            return "ERROR 2";
        }

        private string ConsoleWriteLine( int pointer)
        {
            int offset = 1;
            string output = "";
            bool isReading = true;

            while (isReading)
            {
                string character = Converter.HexToAscii(memory[pointer + offset]);

                if(character == "NUL") return "ERROR -2";

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
