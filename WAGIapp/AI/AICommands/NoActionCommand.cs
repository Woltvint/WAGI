using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WAGIapp.AI.AICommands
{
    internal class NoActionCommand : Command
    {
        public override string Name => "no-action";

        public override string Description => "does nothing";

        public override string Format => "no-action";

        public override async Task<string> Execute(Master caller, string[] args)
        {
            return "command did nothing";
        }
    }
}
