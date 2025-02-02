using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Commands;

namespace src
{
    public static class Utils
    {
        public static CommInvoker InitializeCommands(){
            var commInvoker = new CommInvoker();
            var commReciever = new CommReciever();

            commInvoker.setCommand("init", new InitCommand(commReciever));
            
            return commInvoker;
        }
    }
}