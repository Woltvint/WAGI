using System;
using WAGIapp.AI;

namespace WAGIapp.AI
{
    internal class LongTermMemory
    {
        public List<Memory> memories;
        public HashSet<int> lastUsedMemories;
        public HashSet<string> tags;

        public bool MemoryChanged = false;

        private int maxMemorySize;

        public LongTermMemory(int maxMem = 256)
        {
            memories = new List<Memory>();
            lastUsedMemories = new HashSet<int>();
            tags = new HashSet<string>();
            maxMemorySize = maxMem;
        }

        public async Task MakeMemory(string state)
        {
            string tagString = "Tags:\n";

            foreach (var tag in tags)
                tagString += tag + " ,";
            tagString.TrimEnd(',');
            tagString += "\n";

            ChatMessage tagsListMessage = new ChatMessage(ChatRole.System, tagString);
            ChatMessage stateMessage = new ChatMessage(ChatRole.User, state);

            string mem = await OpenAI.GetChatCompletion(ChatModel.ChatGPT, new List<ChatMessage>() { tagsListMessage, Texts.ShortTermMemoryAddPrompt, stateMessage, Texts.ShortTermMemoryAddFormat });
            mem = Utils.ExtractJson(mem);

            try
            {
                Memory memory = new Memory(Utils.GetObjectFromJson<OutputMemoryAI>(mem));
                memories.Add(memory);

                foreach (var tag in memory.Tags)
                {
                    tags.Add(tag);
                }

                Console.WriteLine("New short term memory:" + mem);
            }
            catch (Exception)
            {
                Console.WriteLine("Add memory failed");
            }

            MemoryChanged = true;
        }

        public async Task<string> GetMemories(string input)
        {
            string memoryInput = "";

            memoryInput += "Available tags:\n";

            foreach (string tag in tags)
                memoryInput += tag + ", ";

            memoryInput += "\nInput:\n";
            memoryInput += input;

            ChatMessage memoryState = new ChatMessage(ChatRole.User, memoryInput);


            string mem;
            OutputMemoryTagsJson memoryTags;
            while (true)
            {
                try
                {
                    mem = await OpenAI.GetChatCompletion(ChatModel.ChatGPT, new List<ChatMessage>() { Texts.ShortTermMemoryGetPrompt, memoryState, Texts.ShortTermMemoryGetFormat });
                    memoryTags = Utils.GetObjectFromJson<OutputMemoryTagsJson>(mem);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Get memory failed - trying again");
                }

            }

            HashSet<string> splitTags = Utils.CleanInput(memoryTags.tags.Split(",")).ToHashSet();

            memories.Sort((memory1, memory2) => MathF.Sign(memory2.GetScore(splitTags) - memory1.GetScore(splitTags)));
            lastUsedMemories.Clear();

            string output = "";

            for (int i = 0; i < memories.Count && output.Split(" ").Length < maxMemorySize; i++)
            {
                output += (i + 1) + ". \n";
                foreach (var tag in memories[i].Tags)
                {
                    output += tag + ",";
                }
                output += "\nScore: " + memories[i].GetScore(splitTags) + "\n";
                memories[i].Remembered += memories[i].GetScore(splitTags);
                output += memories[i].Content + "\n";
                lastUsedMemories.Add(i);
            }

            MemoryChanged = true;
            return output;
        }


        public object GetGraphData()
        {
            MemoryChanged = false;

            GraphMemoryData data = new();

            data.nodes = new List<GraphMemoryNode>();

            for (int i = 0; i < memories.Count; i++)
            {
                data.nodes.Add(new GraphMemoryNode() { id = i, name = memories[i].Tags.First(), text = memories[i].Content, color = lastUsedMemories.Contains(i)? "#0bba83" : "#dddddd" });
            }


            for (int i = 0; i < memories.Count; i++)
            {
                for (int j = 0; j < memories.Count; j++)
                {
                    if (i == j)
                        continue;

                    if (memories[i].GetScore(memories[j].Tags) > 0)
                        data.links.Add(new GraphMemoryLink() { target = i, source = j, label = memories[i].GetCommonTag(memories[j].Tags), width = memories[i].GetScore(memories[j].Tags) });

                }
            }

            return data;
        }
    }
}
