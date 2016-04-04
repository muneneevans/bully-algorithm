using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peer
{
    public class Constants
    {
        public const string Election = "election";
        public const string IWon = "iwon";
        public const string Crash = "crash";
        public const string Message = "message";
        public const string Me = "me";
    }

    public class Container
    {
        public string Header;
        public Process peer;
    }
}
