using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Reciever.Utils
{
    public class ListUtils
    {
        public static void ListStagedChanges(){
            string indexPath = Path.Combine(Directory.GetCurrentDirectory(), ".epoch", "index");

            if (!File.Exists(indexPath)){
                Console.WriteLine("No files staged yet.");
                return;
            }

            Console.WriteLine("Staged files:");
            foreach (string line in File.ReadLines(indexPath)){
                string[] parts = line.Split(' ');
                if (parts.Length == 2){
                    string hash = parts[0];
                    string filePath = parts[1];
                    Console.WriteLine($"[{hash[..6]}] {filePath}"); // Display shortened hash
                }
            }
        }
    }
}