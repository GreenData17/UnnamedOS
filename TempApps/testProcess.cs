using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnnamedOS.TempApps
{
    public class testProcess
    {
        // This is for preparation, so I don't forget how I want to do it later on XD


        public enum priority
        {
            Low,
            Normal,
            High
        }


        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public priority Priority { get; set; }


        public byte[] Code { get; set; }
        public byte[] Memory { get; set; }
        public byte[] Stack { get; set; }
}
}
