/*using OpenAI.Chat;
using OpenAI.Models;
using OpenAI;*/

using System.Net.Http.Headers;
using System.Text.Json;

namespace WAGIapp.AI
{
    internal class OpenAI
    {
        public static async Task<string> GetChatCompletion(string model, List<ChatMessage> messages, int maxTokens = 8192)
        {

            ChatRequest req = new ChatRequest() { messages = messages, model = model };
            ChatResponse res;


            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.openai.com/v1/chat/completions"),
                Headers =
                {
                    { "User-Agent", "WAGIapp" },
                    { "Authorization", "Bearer " + Settings.OpenAIApiKey },
                },

                Content = new StringContent(JsonSerializer.Serialize(req))
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                res = JsonSerializer.Deserialize<ChatResponse>(body);
            }



            return res.choices.First().message.content;
        }
    }

    public struct ChatRequest
    {
        public string model { get; set; }
        public List<ChatMessage> messages { get; set; }
    }

    public struct ChatResponse
    {
        public string id { get; set; }
        public List<ChatChoice> choices { get; set; }
    }

    public struct ChatChoice
    {
        public ChatMessage message { get; set; }
        public string finish_reason { get; set; }
        public int index { get; set; }
    }

    public struct ChatMessage
    {
        public string role { get; set; }
        public string content { get; set; }

        public ChatMessage(string _role, string _content)
        {
            role = _role;
            content = _content;
        }
    }

    public static class ChatRole
    {
        public static string System => "system";
        public static string User => "user";
        public static string Assistant => "assistant";
    }

    public static class ChatModel
    {
        public static string ChatGPT => "gpt-3.5-turbo";
    }
}