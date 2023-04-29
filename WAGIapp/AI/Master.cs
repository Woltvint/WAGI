using System.Text.Json;

namespace WAGIapp.AI
{
    internal class Master
    {
        private static Master singleton;
        public static Master Singleton 
        {
            get 
            {
                if (singleton == null)
                {
                    Console.WriteLine("Create master");
                    singleton = new Master();
                    Console.WriteLine("Master created");
                }

                return singleton;
            }
        }


        public LongTermMemory Memory;
        public ActionList Actions;

        public bool Done = true;

        private string nextPrompt = "";
        private string lastCommandOuput = "";


        public List<string> Notes;

        private List<ChatMessage> LastMessages = new List<ChatMessage>();
        private List<ChatMessage> LastCommand = new List<ChatMessage>();

        public string FormatedNotes
        {
            get
            {
                string output = "";
                for (int i = 0; i < Notes.Count; i++)
                {
                    output += (i + 1) + ". " + Notes[i] + "\n";
                }

                return output;
            }
        }

        public Master()
        {
            Notes = new List<string>();

            Memory = new LongTermMemory(1024);
            Actions = new ActionList(10);

            singleton = this;
        }

        public async Task Tick()
        {

            Console.WriteLine("master tick -master");

            if (Done)
                return;

            if (Memory.memories.Count == 0)
            {
                await Memory.MakeMemory(Settings.Goal);
                Console.WriteLine("master start memory done");
            }

            var masterInput = await GetMasterInput();

            string responseString;
            MasterResponse response;

            var action = Actions.AddAction("Thinking", LogAction.ThinkIcon);

            while (true)
            {
                try
                {
                    responseString = await OpenAI.GetChatCompletion(ChatModel.ChatGPT, masterInput);

                    response = Utils.GetObjectFromJson<MasterResponse>(responseString) ?? new();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Master failed - trying again");
                }
            }

            nextPrompt = response.prompt;

            lastCommandOuput = await Commands.TryToRun(this, response.command);

            LastMessages.Add(new ChatMessage(ChatRole.Assistant, responseString));
            LastCommand.Add(new ChatMessage(ChatRole.System, "Command output:\n" + lastCommandOuput));

            if (LastMessages.Count >= 5)
                LastMessages.RemoveAt(0);

            if (LastCommand.Count >= 5)
                LastCommand.RemoveAt(0);

            action.Text = response.thoughts;

            masterInput.Add(LastMessages.Last());
            masterInput.Add(LastCommand.Last());

            Console.WriteLine(JsonSerializer.Serialize(masterInput, new JsonSerializerOptions() { WriteIndented = true }));
            Console.WriteLine("------------------------------------------------------------------------");

            await Memory.MakeMemory(responseString);

        }

        public async Task<List<ChatMessage>> GetMasterInput()
        {
            List<ChatMessage> messages = new List<ChatMessage>();

            messages.Add(Texts.MasterStartText);
            messages.Add(new ChatMessage(ChatRole.System, "Memories:\n" + await Memory.GetMemories(nextPrompt)));
            messages.Add(new ChatMessage(ChatRole.System, "Notes:\n" + FormatedNotes));
            messages.Add(new ChatMessage(ChatRole.System, "Commands:\n" + Commands.GetCommands()));
            messages.Add(new ChatMessage(ChatRole.System, "Main Goal:\n" + Settings.Goal));
            messages.Add(Texts.MasterStartText);
            messages.Add(Texts.MasterOutputFormat);

            for (int i = 0; i < LastMessages.Count; i++)
            {
                messages.Add(LastMessages[i]);
                messages.Add(LastCommand[i]);
            }

            return messages;
        }
    }


    class MasterResponse
    {
        public string thoughts { get; set; } = "";
        public string prompt { get; set; } = "";
        public string command { get; set; } = "";
    }
}
