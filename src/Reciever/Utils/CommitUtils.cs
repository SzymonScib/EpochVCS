using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace src.Reciever.Utils
{
    public class CommitUtils
    {
        public static List<(string Hash, string Path)> ReadIndex(string indexPath){
            return File.ReadAllLines(indexPath)
                        .Select(line => line.Split(' '))
                        .Select(parts => (Hash: parts[0], Path: parts[1]))
                        .ToList();
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

        
    }
}