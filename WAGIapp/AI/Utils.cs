using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;
using System;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace WAGIapp.AI
{
    internal class Utils
    {
        public static string ExtractJson(string input)
        {
            input = input.Replace("\"\n", "\",\n");

            int firstBracket = input.IndexOf("{");
            int lastBracket = input.LastIndexOf("}");

            if (firstBracket < 0)
            {
                Console.WriteLine("Json extraction failed: missing '{'");
                return "";
            }

            if (lastBracket < 0)
            {
                Console.WriteLine("Json extraction failed: missing '}'");
                return "";
            }

            return input.Substring(firstBracket, lastBracket - firstBracket + 1);
        }

        public static T? GetObjectFromJson<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(ExtractJson(json), new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
            });
        }

        public static IEnumerable<string> CleanInput(IEnumerable<string> input)
        {
            var list = input.ToArray();

            for (int i = 0; i < list.Length; i++)
            {
                list[i] = list[i].Trim();
                list[i] = list[i].ToLower();
            }

            return list;
        }

        public static async Task<string> WebResult(string url, bool corsProxy = false)
        {
            try
            {
                var client = new HttpClient();

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri((corsProxy ? "https://wapi.woltvint.net/cors-proxy?url=" : "") + url),
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    return body;
                }
            }
            catch (Exception)
            {
                return "Web request failed";
            }
        }


        public static string CleanHtmlInput(string input)
        {
            string output = input;

            output = HttpUtility.HtmlDecode(output);
            output = Regex.Replace(output, "<[^>]*>", " ", RegexOptions.ExplicitCapture);
            output = output.Trim();

            return output;
        }

    }
}
