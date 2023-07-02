using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnnamedOS.services.TextMode;

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
            int theme = 0;

            if (isPreview)
                theme = 2;
            
            SetColorTheme(theme);

            Console.Clear();

            SetColorTheme(theme + 1);

            TextDrawService.DrawTitle("PANIC");
            SetColorTheme(theme);
            TextDrawService.NewLine();
            TextDrawService.NewLine();
            TextDrawService.NewLine();
            TextDrawService.NewLine();
            TextDrawService.NewLine();
            TextDrawService.DrawText(message);
            TextDrawService.NewLine();
            TextDrawService.NewLine();
            TextDrawService.DrawText($"process: {process}");
            TextDrawService.NewLine();
            TextDrawService.DrawText($"{hint}");
            TextDrawService.NewLine();
            TextDrawService.NewLine();
            TextDrawService.NewLine();
            TextDrawService.DrawText("Press any key to reboot... [internal] replace shutdown in kernel.cs");

            Console.CursorVisible = false;
        }

        private void SetColorTheme(int theme)
        {
            if (theme == 0)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
            }else if (theme == 1)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Blue;
            }else if (theme == 2)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (theme == 3)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }
    }
}
