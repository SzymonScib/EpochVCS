using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Commands;

namespace src.Invoker
{
    public class CommInvoker
    {
        private readonly Dictionary<string, ICommand> _commands = new();

        public void setCommand(string commandName, ICommand command){
            _commands[commandName] = command;
        }

        public void ExecuteCommand(string commandName){
            if (_commands.TryGetValue(commandName, out var command)){
                command.Execute();
            }
            else{
                throw new InvalidOperationException("Invalid command.");
            }
        }
    }
}