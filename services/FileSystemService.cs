using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;

namespace UnnamedOS.services
{
    public class FileSystemService
    {
        /*      the system doesn't care about the file extenstion. the extension is just for the user.
         *
         *      -- banned chars --
         *      $ = system only files. (the user has no way to access them)
         *
         */


        public static FileSystemService instance { get; private set; }
        private CosmosVFS _fileSystem = new CosmosVFS();

        private const string BANNED_CHARS = "$";

        public enum FileSystemRespond
        {
            Success,
            AlreadyExists,
            NotFound,
            NoAccess,
            IsSystemOnly
        }

        public FileSystemService()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                PanicService.ThrowPanic("FileSystem", "There was an attempt to instance a second filesystem.");
                return;
            }

            VFSManager.RegisterVFS(_fileSystem);
            if (SearchFile(@"0:\sys", "$", true) == FileSystemRespond.Success)
            {
                Console.WriteLine("TRUE");
            }
            else
            {
                // SetupService.Start();
            }
        }

        /* DIRECTORY */

        public string[] GetDirectories(string path, bool isSystem = false)
        {
            if (SearchDirectory(path, "", isSystem) != FileSystemRespond.Success)
                return Array.Empty<string>();

            return Directory.GetDirectories(path);
        }

        public FileSystemRespond CreateDirectory(string path, string name, bool isSystem = false)
        {
            if (!isSystem)
            {
                foreach (var bannedChar in BANNED_CHARS)
                {
                    if (name.Contains(bannedChar))
                    {
                        return FileSystemRespond.IsSystemOnly;
                    }
                }
            }

            if (SearchDirectory(path, name, isSystem) == FileSystemRespond.Success)
                return FileSystemRespond.AlreadyExists;

            Directory.CreateDirectory($"{path}/{name}");

            return FileSystemRespond.Success;
        }

        public FileSystemRespond DeleteDirectory(string path, string name, bool isSystem = false)
        {
            if (!isSystem)
            {
                foreach (var bannedChar in BANNED_CHARS)
                {
                    if (name.Contains(bannedChar))
                    {
                        return FileSystemRespond.IsSystemOnly;
                    }
                }
            }

            if (SearchDirectory(path, name, isSystem) == FileSystemRespond.Success)
                return FileSystemRespond.AlreadyExists;

            Directory.Delete($"{path}/{name}");

            return FileSystemRespond.Success;
        }

        public FileSystemRespond SearchDirectory(string path, string name, bool isSystem = false)
        {
            if (!isSystem)
            {
                foreach (var bannedChar in BANNED_CHARS)
                {
                    if (name.Contains(bannedChar))
                    {
                        return FileSystemRespond.IsSystemOnly;
                    }
                }
            }

            if (name != "")
                path += "/";

            if (Directory.Exists($"{path}{name}"))
                return FileSystemRespond.Success;
            else
                return FileSystemRespond.NotFound;
        }

        /* FILE */

        public string[] GetFiles(string path, bool isSystem = false)
        {
            if (SearchDirectory(path, "", isSystem) != FileSystemRespond.Success)
            {
                Console.WriteLine("not FOUND");
                return Array.Empty<string>();
            }

            return Directory.GetFiles(path);
        }

        public FileSystemRespond CreateFile(string path, string name, bool isSystem = false)
        {
            if(!isSystem){
                foreach (var bannedChar in BANNED_CHARS)
                {
                    if (name.Contains(bannedChar))
                    {
                        return FileSystemRespond.IsSystemOnly;
                    }
                }
            }

            if (SearchFile(path, name, isSystem) == FileSystemRespond.Success)
                return FileSystemRespond.AlreadyExists;

            File.Create($"{path}/{name}");

            return FileSystemRespond.Success;
        }

        public FileSystemRespond WriteFile(string path, string name, string[] content, bool isSystem = false)
        {
            if (!isSystem)
            {
                foreach (var bannedChar in BANNED_CHARS)
                {
                    if (name.Contains(bannedChar))
                    {
                        return FileSystemRespond.IsSystemOnly;
                    }
                }
            }

            if (SearchFile(path, name, isSystem) == FileSystemRespond.Success)
                return FileSystemRespond.AlreadyExists;


            File.WriteAllLines($"{path}/{name}", content);

            return FileSystemRespond.Success;
        }

        public FileSystemRespond DeleteFile(string path, string name, bool isSystem = false)
        {
            if (!isSystem)
            {
                foreach (var bannedChar in BANNED_CHARS)
                {
                    if (name.Contains(bannedChar))
                    {
                        return FileSystemRespond.IsSystemOnly;
                    }
                }
            }

            if (SearchFile(path, name, isSystem) != FileSystemRespond.Success)
                return FileSystemRespond.NotFound;

            File.Delete($"{path}/{name}");

            return FileSystemRespond.Success;
        }

        public FileSystemRespond SearchFile(string path, string name, bool isSystem = false)
        {
            if (!isSystem)
            {
                foreach (var bannedChar in BANNED_CHARS)
                {
                    if (name.Contains(bannedChar))
                    {
                        return FileSystemRespond.IsSystemOnly;
                    }
                }
            }

            if (File.Exists($"{path}/{name}"))
                return FileSystemRespond.Success;
            else
                return FileSystemRespond.NotFound;
        }
    }
}
