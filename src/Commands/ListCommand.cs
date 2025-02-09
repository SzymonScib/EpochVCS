using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Reciever;

namespace src.Commands
{
    public class ListCommand : ICommand
    {
        private readonly CommReciever _commReciever;

        public ListCommand(CommReciever commReciever){
            _commReciever = commReciever;
        }

        public void Execute(){
            _commReciever.List();
        }
    }
}