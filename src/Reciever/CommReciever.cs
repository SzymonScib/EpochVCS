using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace src.Reciever
{
    public class CommReciever
    {
        public void Init(){
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

        public void Stage(string[] fileNames){
            string epochDir = Path.Combine(Directory.GetCurrentDirectory(), ".epoch");

            List<string> filesToStage;

            if (fileNames.Length == 1 && fileNames[0] == "."){
                // Stage all files
                filesToStage = Directory.GetFiles(Directory.GetCurrentDirectory(), "*", SearchOption.AllDirectories)
                                        .Where(f => !f.Contains(".epoch"))
                                        .ToList();
            }
            else{
                // Stage specified files
                filesToStage = fileNames.Select(f => Path.Combine(Directory.GetCurrentDirectory(), f))
                                        .Where(f => File.Exists(f) && !f.Contains(".epoch"))
                                        .ToList();
            }

            RecieverUtils.StageFiles(filesToStage);
        }

        public void Commit(string message){
            string epochDir = Path.Combine(Directory.GetCurrentDirectory(), ".epoch");
            string indexPath = Path.Combine(epochDir, "index");
            string objectsPath = Path.Combine(epochDir, "objects");
            string headPath = Path.Combine(epochDir, "HEAD");
            //TODO: Implement this method
            // 1. Read the staged changes from the index
            var indexEntries = RecieverUtils.ReadIndex(indexPath);
            // 2. Create a tree object
            string treeHashHex = RecieverUtils.CreateTreeObject(indexEntries, objectsPath);
            // 3. Create a commit object
            string parentCommit = File.Exists(headPath) ? File.ReadAllText(headPath).Trim() : "";
            string commitHashHex = RecieverUtils.CreateCommitObject(treeHashHex, parentCommit, message, objectsPath);
            // 4. Store the commit object and update HEAD
            File.WriteAllText(headPath, commitHashHex);
            Console.WriteLine($"[commit {commitHashHex}] {message}");

        }
    }
}