﻿using src.Commands;

namespace src
{
    class Program
    {
        static void Main(string[] args){
            if (args.Length == 0)
            {
                System.Console.WriteLine("No command provided.");
                return;
            }

            var invoker = Utils.InitializeCommands(args);

            invoker.ExecuteCommand(args[0]);
        }
    }
}
