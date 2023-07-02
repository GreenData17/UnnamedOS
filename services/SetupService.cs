using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnnamedOS.services.TextMode;

namespace UnnamedOS.services
{
    public class SetupService
    {
        /*
         *  1. Set PC name.
         *
         *  2. Set Username and Password.
         *
         *  (3.) select if Text or Graphic mode should be used.
         *
         *
         */

        private static string _pcName = "";
        private static string _username = "";
        private static string _password = "";
        private const string BANNED_CHARS = ".,-_<>\\/+\"*ç%&()=?`¦@#°§¬|¢´~^'¨][}{$:;€§°";


        public static void Start()
        {
            TextDrawService.Clear();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            TextDrawService.DrawTitle("Welcome");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            TextDrawService.NewLine();
            TextDrawService.NewLine();

            TextDrawService.DrawText("Please go through this Setup Carefully");
            TextDrawService.NewLine();

            TextDrawService.DrawText("Press any Key to continue...");
            TextDrawService.NewLine();

            TextDrawService.NewLine();
            TextDrawService.NewLine();

            Console.ReadKey(true);

            SetPcName();
        }

        private static void SetPcName()
        {
            TextDrawService.Clear();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            TextDrawService.DrawTitle("my pc is legally called");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            TextDrawService.NewLine();
            TextDrawService.NewLine();

            TextDrawService.DrawText("Please enter how this PC should be called.");
            TextDrawService.DrawText(" (keep empty to set it as 'MyPC')");
            TextDrawService.NewLine();
            Console.Write("    This PC is called: ");
            _pcName = Console.ReadLine();

            foreach (var bannedChar in BANNED_CHARS)
            {
                if (_pcName.Contains(bannedChar))
                {
                    SetPcName();
                }
            }

            if (_pcName == "") _pcName = "MyPC";

            SetUsername();
        }

        private static void SetUsername()
        {
            TextDrawService.Clear();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            TextDrawService.DrawTitle("Who am I?");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            TextDrawService.NewLine();
            TextDrawService.NewLine();

            TextDrawService.DrawText("Please enter your username.");
            TextDrawService.DrawText(" (keep empty to set it as 'admin')");
            TextDrawService.NewLine();
            Console.Write("    This user is called: ");
            _username = Console.ReadLine();

            foreach (var bannedChar in BANNED_CHARS)
            {
                if (_username.Contains(bannedChar))
                {
                    SetUsername();
                }
            }

            if (_username == "") _username = "admin";

            SetPassword();
        }

        private static void SetPassword()
        {
            TextDrawService.Clear();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            TextDrawService.DrawTitle("security? pff-");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            TextDrawService.NewLine();
            TextDrawService.NewLine();

            TextDrawService.DrawText($"Please enter the password for {_username}.");
            TextDrawService.DrawText(" (keep empty to disable password. [NOT RECOMMENDED])");
            TextDrawService.NewLine();
            Console.Write("    The Password is: ");
            _password = Console.ReadLine();

            foreach (var bannedChar in BANNED_CHARS)
            {
                if (_password.Contains(bannedChar))
                {
                    SetPassword();
                }
            }

            InitalizeFileSystem_folder();
        }


        private static void InitalizeFileSystem_folder()
        {
            TextDrawService.Clear();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            TextDrawService.DrawTitle("everything is awsom- DMCA");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            TextDrawService.NewLine();
            TextDrawService.NewLine();

            TextDrawService.DrawText($"Setting up your PC, do not shutdown the pc.");
            TextDrawService.NewLine();
            TextDrawService.DrawDialog(new [] { "" });

            // Delete Cosmos default files
            FileSystemService.instance.DeleteFile(@"0:", "Root.txt", true);
            FileSystemService.instance.DeleteFile(@"0:", "Kudzu.txt", true);
            FileSystemService.instance.DeleteFile(@"0:\TEST\DirInTest", "Readme.txt", true);
            FileSystemService.instance.DeleteDirectory(@"0:\TEST", "DirInTest", true);
            FileSystemService.instance.DeleteDirectory(@"0:", "TEST", true);
            FileSystemService.instance.DeleteDirectory(@"0:", @"0:\Dir Testing", true);


            FileSystemService.instance.CreateDirectory("0:", "sys", true); // system data
            FileSystemService.instance.CreateDirectory("0:", "bin", true); // user applications
            FileSystemService.instance.CreateDirectory("0:", "sbin", true); // system applications
            FileSystemService.instance.CreateDirectory("0:", "lib", true); // libraries
            FileSystemService.instance.CreateDirectory("0:", "dev", true); // Device files. disk, keyboard, etc. in file format.
            FileSystemService.instance.CreateDirectory("0:", "var", true); // variables files, like logs, cache, etc.
            FileSystemService.instance.CreateDirectory("0:", "temp", true); // temporary files. gets cleared before every shutdown.
            FileSystemService.instance.CreateDirectory("0:", "config", true); // basically linux etc folder. (Editable text config)
            FileSystemService.instance.CreateDirectory("0:", "home", true); // user environment
            FileSystemService.instance.CreateDirectory(@"0:\home", _username, true); // user
            FileSystemService.instance.CreateDirectory(@$"0:\home\{_username}", "Documents", true); // user - documents
            FileSystemService.instance.CreateDirectory(@$"0:\home\{_username}", ".bin_save", true); // user - save place for apps

            InitalizeFileSystem_file();
        }

        private static void InitalizeFileSystem_file()
        {
            TextDrawService.Clear();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            TextDrawService.DrawTitle("everything is awsom- DMCA");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            TextDrawService.NewLine();
            TextDrawService.NewLine();

            TextDrawService.DrawText($"Setting up your PC, do not shutdown the pc.");
            TextDrawService.NewLine();
            TextDrawService.DrawDialog(new[] { "@@@@@@" });

            FileSystemService.instance.WriteFile(@"0:\sys", $"${_username}", new []{$"password: {_password}"}, true); // save user and password
            FileSystemService.instance.WriteFile(@"0:\config", $"exe_location", new []{@"0:\bin", @"0:\sbin"}, true); // config - app locations
            FileSystemService.instance.WriteFile(@"0:\config", $"console", new []{"background: Black", "color: White", "welcome_msg: 'welcome back ^^'", "console_prefix: '$1 > '"}, true); // config - console config
            FileSystemService.instance.WriteFile(@"0:\sys", $"$", new []{""}, true); // indicates that a setup has happened

            InitalizeFileSystem_finish();
        }

        private static void InitalizeFileSystem_finish()
        {
            TextDrawService.Clear();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            TextDrawService.DrawTitle("everything is awsom- DMCA");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            TextDrawService.NewLine();
            TextDrawService.NewLine();

            TextDrawService.DrawText($"your PC has been setup. press any key to get into the system.");
            TextDrawService.NewLine();
            TextDrawService.DrawDialog(new[] { "@@@@@@@@@@@@" });

            Console.ReadLine();
            TextDrawService.Clear();
        }
    }
}
