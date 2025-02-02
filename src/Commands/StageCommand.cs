using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Commands
{
    public class StageCommand : ICommand
    {
        private readonly CommReciever _commReciever;

        public StageCommand(CommReciever commReciever){
            _commReciever = commReciever;
        }

        public void Execute(){
            _commReciever.Stage();
        }
    }
}