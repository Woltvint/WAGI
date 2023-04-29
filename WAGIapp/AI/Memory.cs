namespace WAGIapp.AI
{
    internal class Memory
    {
        public string Content { get; private set; }
        public HashSet<string> Tags { get; private set; }
        public int Remembered { get; set; }

        public Memory(string content, HashSet<string> tags)
        {
            Content = content;
            Tags = tags;
            Remembered = 0;
        }

        public Memory(OutputMemoryAI m)
        {
            Content = m.content;
            Tags = Utils.CleanInput(m.tags.Split(",")).ToHashSet();
        }

        public int GetScore(HashSet<string> tags)
        {
            int output = 0;
            foreach (string tag in tags)
            {
                if (Tags.Contains(tag))
                    output++;
            }

            return output;
        }

        public string GetCommonTag(HashSet<string> tags)
        {
            foreach (string tag in tags)
            {
                if (Tags.Contains(tag))
                    return tag;
            }

            return "";
        }

        public override string ToString()
        {
            return Content;
        }

    }

    internal struct OutputMemoryAI
    {
        public string tags { get; set; }
        public string content { get; set; }
    }

    internal struct OutputMemoryTagsJson
    {
        public string tags { get; set; }
    }

    internal struct GraphMemoryData
    {
        public List<GraphMemoryNode> nodes { get; set; }
        public List<GraphMemoryLink> links { get; set; }

        public GraphMemoryData()
        {
            nodes = new List<GraphMemoryNode>();
            links = new List<GraphMemoryLink>();
        }
    }

    internal struct GraphMemoryNode
    {
        public int id { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public string color { get; set; }
    }

    internal struct GraphMemoryLink
    {
        public string label { get; set; }
        public int width { get; set; }
        public int source { get; set; }
        public int target { get; set; }
    }
}