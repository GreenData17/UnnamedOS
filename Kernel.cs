using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sys = Cosmos.System;
using UnnamedOS.services;
using UnnamedOS.TempApps;

namespace UnnamedOS
{
    public class Kernel : Sys.Kernel
    {
        private PanicService _panic;

        private FileSystemService _fileSystem;

        private TempTextConsole _console;

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
            _panic = new PanicService();
            _fileSystem = new FileSystemService();
            _console = new TempTextConsole();
        }

        protected override void Run()
        {
            if(_panic == null ) { Sys.Power.Shutdown(); }
            else if (PanicService.panic) { Console.ReadLine(); Sys.Power.Shutdown(); }

            _console.Update();

            //Console.Write("Input: ");
            //var input = Console.ReadLine();
            //if (input == "") { Sys.Power.Shutdown(); }
            //else if (input == "panic") { PanicService.ThrowPanic("test", "this is a test panic", "", true); }
            //else
            //{
            //    Console.Write("Text typed: ");
            //    Console.WriteLine(input);
            //}

        }
    }
}
