using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAGIapp.AI.AICommands
{
    internal class AddNoteCommand : Command
    {
        public override string Name => "add-note"; 

        public override string Description => "Adds a note to the list"; 

        public override string Format => "add-note | text to add to the list";

        public override async Task<string> Execute(Master caller, string[] args)
        {
            if (args.Length < 2)
                return "error! not enough parameters";

            caller.Notes.Add(args[1]);

            return "Note added";
        }
    }
}
