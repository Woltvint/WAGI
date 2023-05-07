using WAGIapp.AI.AICommands;

namespace WAGIapp.AI
{
    internal class Commands
    {
        private static Dictionary<string, Command> commands;
        static Commands()
        {
            commands = new Dictionary<string, Command>();

            Command c;

            c = new NoActionCommand();
            commands.Add(c.Name, c);
            c = new GoalReachedCommand();
            commands.Add(c.Name, c);
            c = new AddNoteCommand();
            commands.Add(c.Name, c);
            c = new RemoveNoteCommand();
            commands.Add(c.Name, c);
            c = new SearchWebCommand();
            commands.Add(c.Name, c);
            c = new RemoveLineCommand();
            commands.Add(c.Name, c);
            c = new WriteLineCommand();
            commands.Add(c.Name, c);

        }


        public static string GetCommands()
        {
            string output = "";

            foreach (Command c in commands.Values)
            {
                output += c.Name + "\n";
                output += "\t" + c.Description + "\n";
                output += "\t" + "Call format: " + c.Format + "\n";
            }

            return output;
        }

        public static async Task<string> TryToRun(Master caller, string commandText)
        {
            string[] args = commandText.Split("|");

            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i].Trim();
            }

            args[0] = args[0].ToLower();

            caller.Actions.AddAction("Running cmd: " + args[0], LogAction.CommandIcon);

            if (!commands.ContainsKey(args[0]))
                return "error! command not found";

            return await commands[args[0]].Execute(caller, args);
        }

    }
}
