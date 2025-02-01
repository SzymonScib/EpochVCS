using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Commands
{
    public interface ICommand
    {
        public void Execute();
    }
}