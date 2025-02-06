using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Reciever;

namespace src.Commands
{
    public class CommmitCommand : ICommand
    {
        private readonly CommReciever _commReciever;
        private readonly string _message;
        public CommmitCommand(CommReciever commReciever, string message){
            _commReciever = commReciever;
            _message = message;
        }
        public void Execute(){    
            _commReciever.Commit(_message);
        }  
    }
}