using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnnamedOS.services.TextMode
{
    public class TextDrawService
    {
        public const int WIDTH = 82;
        public const int HEIGHT = 22;

        public static void Clear() => Console.Clear();

        public static void DrawText(string text)
        {
            Console.WriteLine($"    {text}");
        }

        public static void DrawDialog(string[] message)
        {
            int width = WIDTH - 8;

            Console.WriteLine("    ");
            for (int i = 0; i < width; i++)
            {
                Console.Write("#");
            }
            Console.Write("    ");


            Console.WriteLine("    #");
            for (int i = 0; i < width - 2; i++)
            {
                Console.Write(" ");
            }
            Console.Write("#    ");

            foreach (var msg in message)
            {
                int spacing = width - msg.Length - 2;

                Console.WriteLine("    #");
                Console.WriteLine(msg);
                for (int i = 0; i < spacing - 2; i++)
                {
                    Console.Write(" ");
                }
                Console.Write("#    ");
            }

            Console.WriteLine("    #");
            for (int i = 0; i < width - 2; i++)
            {
                Console.Write(" ");
            }
            Console.Write("#    ");


            Console.WriteLine("    ");
            for (int i = 0; i < width; i++)
            {
                Console.Write("#");
            }
            Console.Write("    ");
        }

        public static void NewLine()
        {
            Console.WriteLine();
        }

        public static void DrawTitle(string title)
        {
            int spacing = 0;
            if (title.Length % 2 == 0)
            {
                spacing = (WIDTH / 2) - (title.Length / 2);
            }
            else
            {
                int tempTitleLength = title.Length + 1;
                spacing = (WIDTH / 2) - (tempTitleLength / 2);
            }

            spacing -= 2;

            for (int i = 0; i < spacing; i++)
            {
                Console.Write(" ");
            }

            Console.Write($" {title} ");

            for (int i = 0; i < spacing; i++)
            {
                Console.Write(" ");
            }

            if (spacing % 2 == 0)
                Console.Write(" ");

            Console.WriteLine();
        }
    }
}
