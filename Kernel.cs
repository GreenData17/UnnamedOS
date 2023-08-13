using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnnamedOS.Core;
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


        public static Kernel instance;
        public List<Process> processes = new List<Process>();

        protected override void BeforeRun()
        {
            instance = this;
            _panic = new PanicService();
            _fileSystem = new FileSystemService();
            _console = new TempTextConsole();
        }

        protected override void Run()
        {
            if(_panic == null ) { Sys.Power.Shutdown(); }

            _console.Update();

            foreach (Process process in processes)
            {
                process.Update();
            }
        }
    }
}
