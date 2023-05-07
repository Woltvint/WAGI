namespace WAGIapp.AI.AICommands
{
    internal class WriteLineCommand : Command
    {
        public override string Name => "write-line";

        public override string Description => "writes the given text to the line number";

        public override string Format => "write-line | line number | text";

        public override async Task<string> Execute(Master caller, string[] args)
        {
            if (args.Length < 3)
                return "error! not enough parameters";

            int line;

            try
            {
                line = Convert.ToInt32(args[1]);
            }
            catch (Exception)
            {
                return "error! given line number is not a number";
            }

            return caller.scriptFile.WriteLine(line, args[2]);
        }
    }
}
