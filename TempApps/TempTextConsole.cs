using Cosmos.System.ExtendedASCII;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System;
using Microsoft.VisualBasic;
using Console = System.Console;
using UnnamedOS.services;

namespace UnnamedOS.TempApps
{
    public class TempTextConsole
    {
        private const int WIDTH = 80;
        private const int HEIGHT = 24;

        private string _currentDirectory = "0:";
        private string _prefix = " $1 > ";

        private List<string> _output = new List<string>();
        private string _input = "";
        private int _cursorX = 0;
        private int _cursorY = 0;


        public TempTextConsole()
        {
            Encoding.RegisterProvider(CosmosEncodingProvider.Instance);
            Console.InputEncoding = Encoding.GetEncoding(437);
            Console.OutputEncoding = Encoding.GetEncoding(437);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            PrintPrefix();
        }

        public void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (!KeyboardManager.TryReadKey(out var keyEvent)) return;


            if (keyEvent.Key == ConsoleKeyEx.Enter)
                EnterPressed(keyEvent);

            else if (keyEvent.Key is ConsoleKeyEx.LeftArrow or ConsoleKeyEx.RightArrow)
                MoveCursor(keyEvent);

            else if (keyEvent.Key == ConsoleKeyEx.Delete)
                DeletePressed();

            else if (keyEvent.Key == ConsoleKeyEx.Backspace)
                BackSpacePressed();

            else
                WriteInput(keyEvent);
        }

        private void PrintToConsole(string text, bool hasPrefix = false)
        {
            AddOutput(text, hasPrefix);
            _cursorY++;
            _cursorX = 0;
            SetCursorPosition(_cursorX, _cursorY, true);
        }

        #region inputAction

        private void EnterPressed(KeyEvent keyEvent)
        {
            PrintToConsole(_input, true);

            HandleCommands();

            _input = "";
            PrintPrefix();
        }

        private void MoveCursor(KeyEvent keyEvent)
        {
            if (keyEvent.Key == ConsoleKeyEx.LeftArrow)
            {
                if (_cursorX > 0)
                {
                    _cursorX -= 1;
                    SetCursorPosition(_cursorX, _cursorY);
                }

                Console.CursorSize = 100;
            }
            else if (keyEvent.Key == ConsoleKeyEx.RightArrow)
            {
                if (_cursorX < _input.Length)
                {
                    _cursorX += 1;
                    SetCursorPosition(_cursorX, _cursorY);
                }

                if (_cursorX == _input.Length)
                {
                    Console.CursorSize = 20;
                }
            }
        }

        private void DeletePressed()
        {
            if (_cursorX == _input.Length) return;
            if (_cursorX == _input.Length - 1) Console.CursorSize = 20;

            string inputAfterCursor = _input.Substring(_cursorX + 1);
            _input = _input.Substring(0, _cursorX) + inputAfterCursor;

            SetCursorPosition(_cursorX, _cursorY);
            Console.Write(inputAfterCursor + " ");
            SetCursorPosition(_cursorX, _cursorY);
        }

        private void BackSpacePressed()
        {
            if (_cursorX == 0) return;

            string inputAfterCursor = _input.Substring(_cursorX);
            _cursorX -= 1;
            _input = _input.Substring(0, _cursorX) + inputAfterCursor;

            SetCursorPosition(_cursorX, _cursorY);
            Console.Write(inputAfterCursor + " ");
            SetCursorPosition(_cursorX, _cursorY);
        }

        private void WriteInput(KeyEvent keyEvent)
        {
            if(GetPrefixString().Length + _input.Length == WIDTH) return;

            if (_cursorX == _input.Length)
            {
                _input += keyEvent.KeyChar;
                _cursorX += 1;
                Console.Write(keyEvent.KeyChar);
            }
            else
            {
                string inputAfterCursor = _input.Substring(_cursorX);

                _input = _input.Insert(_cursorX, keyEvent.KeyChar.ToString());
                _cursorX += 1;

                Console.Write(keyEvent.KeyChar + inputAfterCursor);

                SetCursorPosition(_cursorX, _cursorY);
            }
        }

        #endregion

        private void HandleCommands()
        {
            if (_input == "shutdown")
                Power.Shutdown();
            else if (_input == "test")
            {
                Console.Write("TEST");
                PrintToConsole("TEST");
            }
        }

        // Temp copy, for later redo
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

        private void ScrollOutput()
        {
            _output.RemoveAt(0);

            Console.Clear();
            Console.CursorVisible = false;

            for (int i = 0; i < HEIGHT - 1; i++)
            {
                SetCursorPosition(0, i, true);
                Console.WriteLine(_output[i]);
            }

            Console.CursorVisible = true;
            Console.CursorSize = 20;

            _cursorY -= 1;
        }

        private void AddOutput(string output, bool hasPrefix = false)
        {
            string prefix = "";
            if (hasPrefix) prefix = GetPrefixString();

            if(_output.Count < HEIGHT)
                _output.Add(prefix + output);

            if (_output.Count == HEIGHT)
                ScrollOutput();
        }

        private string GetPrefixString()
        {
            return _prefix.Replace("$1", _currentDirectory);
        }

        private void PrintPrefix()
        {
            Console.Write(_prefix.Replace("$1", _currentDirectory));
        }

        private void SetCursorPosition(int X, int Y, bool useRealPosition = false)
        {
            int XOffset = 0;
            if (!useRealPosition) XOffset = GetPrefixString().Length;

            Console.SetCursorPosition(XOffset + X, Y);
        }
    }
}
