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
        public const string Check = "check";
        public const string Message = "message";
    }

    public class Container
    {
        public string Header;
        public Process peer;
    }
}
