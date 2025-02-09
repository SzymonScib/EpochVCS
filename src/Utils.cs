using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Commands;
using src.Reciever;
using src.Invoker;

namespace src
{
    public static class Utils
    {
        public static CommInvoker InitializeCommands(string[] args){
            var commInvoker = new CommInvoker();
            var commReciever = new CommReciever();

            if (args.Length > 0){
                var command = args[0];
                commInvoker.setCommand(command, command switch{
                    "init" => new InitCommand(commReciever),
                    "stage" => new StageCommand(commReciever, args.Skip(1).ToArray()),
                    "commit" => new CommmitCommand(commReciever, string.Join(" ", args.Skip(1))),
                    "list" => new ListCommand(commReciever),
                    _ => throw new ArgumentException($"Unknown command: {command}")
                });
            }

            return commInvoker;
        }
    }
}