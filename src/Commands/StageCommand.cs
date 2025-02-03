using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Reciever;

namespace src.Commands
{
    public class StageCommand : ICommand
    {
        private readonly CommReciever _commReciever;
        private readonly string[] _fileNames;

        public StageCommand(CommReciever commReciever, string[] fileNames){
            _commReciever = commReciever;
            _fileNames = fileNames;
        }

        public void Execute(){
            _commReciever.Stage(_fileNames);
        }
    }
}