using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnnamedOS.services
{
    public class PanicService
    {
        public static PanicService instance { get; private set; }

        public static bool panic = false;

        public const int WIDTH = 82;
        public const int HEIGHT = 22;

        public PanicService()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Cosmos.System.Power.Shutdown();
            }
        }

        public static void ThrowPanic(string process, string message, string hint = "", bool isPreview = false)
        {
            if (instance == null)
            {
                instance = new PanicService();
            }
            
            instance.Panic(process, message, hint, isPreview);
        }

        public void Panic(string process, string message, string hint = "", bool isPreview = false)
        {
            panic = true;
            SetPreviewTheme(isPreview);
            Console.Clear();

            Console.WriteLine("The kernel is panicking");
            Console.WriteLine($"\"{message}\"");
            Console.WriteLine();
            Console.WriteLine($"process: {process}");

            if (hint != "")
            {
                Console.WriteLine($"\"{hint}\"");
            }
            
            Console.WriteLine();
            Console.WriteLine("Press any key to reboot...");

            Console.CursorVisible = false;

            Console.ReadKey();

            Cosmos.System.Power.Reboot();
        }

        private void SetPreviewTheme(bool isPreview)
        {
            if (isPreview)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
