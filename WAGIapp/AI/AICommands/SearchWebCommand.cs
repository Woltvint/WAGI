using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAGIapp.AI.AICommands
{
    internal class SearchWebCommand : Command
    {
        public override string Name => "search-web";

        public override string Description => "Searches the web and returns a list of links and descriptions";

        public override string Format => "search-web | querry";

        public override async Task<string> Execute(Master caller, string[] args)
        {
            if (args.Length < 2)
                return "error! not enough parameters";

            string web = await Utils.WebResult("https://html.duckduckgo.com/html/?q=" + args[1],true);

            List<string> headers = new List<string>();
            List<string> urls = new List<string>();
            List<string> descritpions = new List<string>();

            int i = 0;
            while (true)
            {
                int tagStart = web.IndexOf("<a rel=\"nofollow\" class=\"result__a\"", i);

                if (tagStart == -1)
                    break;

                int tagEnd = web.IndexOf(">", tagStart);

                int blockEnd = web.IndexOf("</a>", tagEnd);

                headers.Add(web.Substring(tagEnd + 1, blockEnd - tagEnd - 1));

                i = blockEnd;
            }


            i = 0;
            while (true)
            {
                int tagStart = web.IndexOf("<a class=\"result__url\"", i);

                if (tagStart == -1)
                    break;

                int tagEnd = web.IndexOf(">", tagStart);

                int blockEnd = web.IndexOf("</a>", tagEnd);

                urls.Add(web.Substring(tagEnd + 1, blockEnd - tagEnd - 1));

                i = blockEnd;
            }


            i = 0;
            while (true)
            {
                int tagStart = web.IndexOf("<a class=\"result__snip", i);

                if (tagStart == -1)
                    break;

                int tagEnd = web.IndexOf(">", tagStart);

                int blockEnd = web.IndexOf("</a>", tagEnd);

                descritpions.Add(web.Substring(tagEnd + 1, blockEnd - tagEnd - 1));

                i = blockEnd;
            }

            string output = "";

            for (int j = 0; j < headers.Count; j++)
            {
                headers[j] = Utils.CleanHtmlInput(headers[j]);
                urls[j] = Utils.CleanHtmlInput(urls[j]);
                descritpions[j] = Utils.CleanHtmlInput(descritpions[j]);
            }

            i = 0;
            while (output.Split(" ").Length < 800 && i < headers.Count)
            {
                output += (i + 1) + ". " + headers[i] + "\n";
                output += "[" + urls[i] + "]\n";
                output += descritpions[i] + "\n\n";
                i++;
            }

            return output;
        }
    }
}
