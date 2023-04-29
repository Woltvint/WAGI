using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAGIapp.AI.AICommands
{
    internal class GoalReachedCommand : Command
    {
        public override string Name => "goal-reached";

        public override string Description => "Command that you must call when you reach the main goal";

        public override string Format => "goal-reached";

        public override async Task<string> Execute(Master caller, string[] args)
        {
            caller.Done = true;
            return "done.";
        }
    }
}
