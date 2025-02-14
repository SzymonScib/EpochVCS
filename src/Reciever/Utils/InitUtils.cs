using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace src.Reciever.Utils
{
    public class InitUtils
    {
        public static void InitialiseRepository(){
            string epochDir = Path.Combine(Directory.GetCurrentDirectory(), ".epoch");

            if (Directory.Exists(epochDir)){
                Console.WriteLine("Epoch is already initialized.");
                return;
            }

            Directory.CreateDirectory(epochDir);
            Directory.CreateDirectory(Path.Combine(epochDir, "objects"));
            Directory.CreateDirectory(Path.Combine(epochDir, "refs"));
            Directory.CreateDirectory(Path.Combine(epochDir, "commits"));

            File.WriteAllText(Path.Combine(epochDir, "HEAD"), "ref: refs/heads/main\n");

            File.WriteAllText(Path.Combine(epochDir, "config"), "[core]\nrepositoryformatversion = 0\n");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
                File.SetAttributes(epochDir, File.GetAttributes(epochDir) | FileAttributes.Hidden);
            }

            Console.WriteLine("Initialized empty VCS repository in " + epochDir);
        }
    }
}