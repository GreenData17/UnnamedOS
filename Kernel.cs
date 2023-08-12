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

            _console.Update();
        }
    }
}
