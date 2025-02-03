using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Reciever;

namespace src.Commands
{
    public class InitCommand : ICommand
    {
        private readonly CommReciever _commReciever;

        public InitCommand(CommReciever commReciever){
            _commReciever = commReciever;
        }

        public void Execute(){
            _commReciever.Init();
        }
    }
}