using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System;
using Cosmos.System.ExtendedASCII;
using UnnamedOS.services;
using Console = System.Console;

namespace UnnamedOS.TempApps
{
    public class TempConsole
    {
        private const int WIDTH = 82;
        private const int HEIGHT = 24;

        private string[] _output = new string[HEIGHT];
        private string _input = "";

        private int cursor = 0;
        private List<string> commandHistory = new List<string>();
        private int commandHistoryPointer = 0;

        private string currentPath = @"0:\";

        public TempConsole()
        {
            Encoding.RegisterProvider(CosmosEncodingProvider.Instance);
            Console.InputEncoding = Encoding.GetEncoding(437);
            Console.OutputEncoding = Encoding.GetEncoding(437);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.CursorVisible = false;
            commandHistory.Add("");

            for (int i = 0; i < _output.Length; i++)
            {
                _output[i] = "";
            }

            UpdateScreen();
        }

        public void Update()
        {
            GetInput();
        }

        private void UpdateScreen()
        {
            Console.Clear();

            foreach (var output in _output)
            {
                if(output == "") continue;
                Console.WriteLine(output);
            }

            Console.Write($" {currentPath} > ");

            for (int i = 0; i < _input.Length; i++)
            {
                if (i == _input.Length) continue;


                if (i == cursor)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.Write(_input[i]);

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write(_input[i]);
                }


            }

            if (cursor == _input.Length) Console.Write("_");
            Console.WriteLine();
            
        }

        private void Print(string text)
        {
            if (_output[^1] != "")  // if last line reached, delete first and move everything up
            {
                for (int i = 0; i < _output.Length; i++)
                {
                    if (i == _output.Length - 1)
                    {
                        _output[i] = "";
                        continue;
                    }

                    _output[i] = _output[i + 1];
                }
            }

            for (int i = 0; i < _output.Length; i++)  // print text
            {
                if (_output[i] != "") continue;

                _output[i] = $"{text}";


                break;
            }
        }

        private void EnterPressed()
        {
            if(_input == "shutdown") Power.Shutdown();

            Print($" {currentPath} > {_input}");

            HandleCommands(_input);

            commandHistory.Insert(commandHistory.Count - 1, _input);
            commandHistoryPointer = commandHistory.Count;

            _input = "";
            cursor = 0;
        }


        private void GetInput()
        {
            if (!KeyboardManager.TryReadKey(out var keyEvent)) return;


            if (keyEvent.Key == ConsoleKeyEx.Backspace)  // remove last character in input
            {
                if (_input.Length > 0)
                {
                    if (cursor == _input.Length)
                    { 
                        _input = _input.Remove(_input.Length - 1, 1);
                        cursor -= 1;
                    }
                    else if(cursor > 0)  // remove character at the cursor in input
                    {
                        string tempInput = _input.Substring(0, cursor - 1);
                        tempInput += _input.Substring(cursor);
                        _input = tempInput;
                        cursor -= 1;
                    }
                       
                }
            }
            else if (keyEvent.Key == ConsoleKeyEx.Enter)  // run commands
            {
                EnterPressed();
            }
            else if (keyEvent.Key == ConsoleKeyEx.LeftArrow)  // move cursor left
            {
                if (cursor > 0)
                    cursor -= 1;
            }
            else if (keyEvent.Key == ConsoleKeyEx.RightArrow)  // move cursor right
            {
                if (cursor < _input.Length)
                    cursor += 1;
            }
            else if (keyEvent.Key == ConsoleKeyEx.DownArrow)  // go down the history
            {
                if (commandHistoryPointer > 0)
                {
                    commandHistoryPointer -= 1;
                    _input = commandHistory[commandHistoryPointer];
                    cursor = commandHistory[commandHistoryPointer].Length;
                }
                else
                {
                    _input = "";
                    cursor = 0;
                }
            }
            else if (keyEvent.Key == ConsoleKeyEx.UpArrow)  // go up the history
            {
                if (commandHistoryPointer < commandHistory.Count - 1)
                {
                    commandHistoryPointer += 1;
                    _input = commandHistory[commandHistoryPointer];
                    cursor = commandHistory[commandHistoryPointer].Length;
                }
                else
                {
                    _input = "";
                    cursor = 0;
                }
            }
            else
            {
                if (cursor == _input.Length)  // if courser has not been moved, add character to input
                {
                    _input += keyEvent.KeyChar;
                    cursor += 1;
                }
                else  // insert character at cursor
                {
                    string tempInput = _input.Substring(0, cursor);
                    tempInput += keyEvent.KeyChar;
                    tempInput += _input.Substring(cursor);
                    _input = tempInput;
                    cursor += 1;
                }
            }

            UpdateScreen();
            
        }

        private void HandleCommands(string input)
        {
            string[] command = input.Split(' ');

            if (command[0].StartsWith("ls")) cmd_ls();
            else if (command[0].StartsWith("exe")) cmd_exe(input);
        }

        private void cmd_ls()
        {
            string[] folders = FileSystemService.instance.GetDirectories(currentPath, true);
            string outputString = "";
            foreach (var folder in folders)
            {
                if(folder.Length > 10)
                    outputString += $"        {folder.Substring(0,10)}";
                else
                    outputString += $"        {folder}";
            }
            string[] files = FileSystemService.instance.GetFiles(currentPath, true);
            foreach (var file in files)
            {
                if (file.Length > 10)
                    outputString += $"        {file.Substring(0, 10)}";
                else
                    outputString += $"        {file}";
            }
            Print(outputString);
        }

        private void cmd_exe(string input)
        {
            string[] command = input.Split(' ');
            List<byte> code = new List<byte>();

            for (int i = 1; i < command.Length; i++)
            {
                code.Add((byte)int.Parse(command[i]));
            }

            OpCodeinterpreter.ExecuteCode(code.ToArray());
            code.Clear();
            code = null;
        }
    }
}
