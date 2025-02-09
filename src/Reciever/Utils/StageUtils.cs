using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace src.Reciever.Utils
{
    public class StageUtils
    {
        public static void StageFiles(List<string> filesToStage){
            string epochDir = Path.Combine(Directory.GetCurrentDirectory(), ".epoch");
            string indexPath = Path.Combine(epochDir, "index");
            string objectsPath = Path.Combine(epochDir, "objects");

            using (var indexFile = new FileStream(indexPath, FileMode.Create, FileAccess.Write))
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
    }
}