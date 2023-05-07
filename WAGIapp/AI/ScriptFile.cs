namespace WAGIapp.AI
{
    public class ScriptFile
    {
        public List<string> Lines { get; set; }

        public ScriptFile() 
        {
            Lines = new List<string>();

            for (int i = 0; i < 21; i++)
                Lines.Add(string.Empty);
        }

        public string GetText()
        {
            string output = "";

            for (int i = 0; i < Lines.Count; i++)
            {
                output += i.ToString("D2") + " | " + Lines[i] + "\n";
            }

            return output;
        }


        public string WriteLine(int line, string text)
        {
            for (int i = 0; i < 100; i++)
            {
                if (line <= Lines.Count)
                    break;

                Lines.Add("");
            }


            string[] lines = text.Split('\n');

            if (line == Lines.Count)
                Lines.Add(lines[0]);
            else
                Lines[line] = lines[0];

            if (lines.Length != 1)
            {
                for (int i = 1; i < lines.Length; i++)
                {
                    if (line + i == Lines.Count)
                        Lines.Add(lines[i]);
                    else
                        Lines.Insert(line + i, lines[i]);
                }
            }

            return "Line written";
        }

        public string RemoveLine(int line) 
        {
            if (line >= Lines.Count)
                return "error! line outside of bounds";

            Lines.RemoveAt(line);

            return "Line removed";
        }
    }
}
