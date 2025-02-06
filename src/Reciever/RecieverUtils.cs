using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace src.Reciever
{
    public class RecieverUtils
    {
        public static void StageFiles(List<string> filesToStage){
            string epochDir = Path.Combine(Directory.GetCurrentDirectory(), ".epoch");
            string indexPath = Path.Combine(epochDir, "index");
            string objectsPath = Path.Combine(epochDir, "objects");

            using (var indexFile = new FileStream(indexPath, FileMode.OpenOrCreate, FileAccess.Write))
            using (var writer = new StreamWriter(indexFile)){
                foreach (var file in filesToStage){
                    // 2. Hash each file's content (SHA-1 Hashing)
                    byte[] fileContent = File.ReadAllBytes(file);
                    byte[] fileHash;
                    using (SHA1 sha1 = SHA1.Create()){
                        fileHash = sha1.ComputeHash(fileContent);
                    }

                    // 3. Make a blob of each file
                    string hashHex = BitConverter.ToString(fileHash).Replace("-", "").ToLower();

                    string blobDir = Path.Combine(objectsPath, hashHex.Substring(0, 2));
                    string blobPath = Path.Combine(blobDir, hashHex.Substring(2));
                    if (!Directory.Exists(blobDir)){
                        Directory.CreateDirectory(blobDir);
                    }
                    if (!File.Exists(blobPath)){
                        File.WriteAllBytes(blobPath, fileContent);
                    }

                    // 4. Construct a tree of blobs based on UNIX file system
                    string relativePath = Path.GetRelativePath(Directory.GetCurrentDirectory(), file);
                    string treeEntry = $"{hashHex} {relativePath}";

                    // 5. Save the tree to ./epoch/objects
                    // (Already done by writing blobs to objects directory)

                    // 6. Store references to blobs in ./epoch/index
                    writer.WriteLine(treeEntry);
                }
            }
        }

        public static List<(string Hash, string Path)> ReadIndex(string indexPath){
            return File.ReadAllLines(indexPath)
                        .Select(line => line.Split(' '))
                        .Select(parts => (Hash: parts[0], Path: parts[1]))
                        .ToList();
        }

        public static string CreateTreeObject(List<(string Hash, string Path)> indexEntries, string objectPath){
            var treeEntries = indexEntries.Select(entry => $"{entry.Hash} {entry.Path}");
            string treeContent = string.Join('\n', treeEntries);
            byte[] treeHash;
            using (SHA1 sha1 = SHA1.Create()){
                treeHash = sha1.ComputeHash(Encoding.UTF8.GetBytes(treeContent));
            }

            string treeHashHex = BitConverter.ToString(treeHash).Replace("-", "").ToLower();

            string treeBlobDir = Path.Combine(objectPath, treeHashHex.Substring(0, 2));
            string treeBlobPath = Path.Combine(treeBlobDir, treeHashHex.Substring(2));

            if (!Directory.Exists(treeBlobDir)){
                Directory.CreateDirectory(treeBlobDir);
            }
            File.WriteAllText(treeBlobPath, treeContent);

            return treeHashHex;
        }

        public static string CreateCommitObject(string treeHashHex, string parentCommit, string message, string objectPath){
            string commitContent = $"tree {treeHashHex}\n";
            if (!string.IsNullOrEmpty(parentCommit) && !parentCommit.StartsWith("ref:")){
                commitContent += $"parent {parentCommit}\n";
            }
            commitContent += $"\n\n{message}";
            
            byte[] commitHash;
            using (SHA1 sha1 = SHA1.Create()){
                commitHash = sha1.ComputeHash(Encoding.UTF8.GetBytes(commitContent));
            }

            string commitHashHex = BitConverter.ToString(commitHash).Replace("-", "").ToLower();
            string commitBlobDir = Path.Combine(objectPath, commitHashHex.Substring(0, 2));
            string commitBlobPath = Path.Combine(commitBlobDir, commitHashHex.Substring(2));
            if(!Directory.Exists(commitBlobDir)){
                Directory.CreateDirectory(commitBlobDir);
            }
            File.WriteAllText(commitBlobPath, commitContent);

            return commitHashHex;
        }
    }
}