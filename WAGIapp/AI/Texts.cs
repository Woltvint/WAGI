namespace WAGIapp.AI
{
    public static class Texts
    {

        public static ChatMessage MasterStartText => new(ChatRole.System, Settings.Rules);

        public static ChatMessage MasterOutputFormat = new ChatMessage(
            ChatRole.System,
            "only reply in this json format" +
            "Output format:" +
            "{" +
            "\"thoughts\": \"your thoughts\"" +
            "\"command\": \"command you want to execute in the format specified for that command\"" +
            "}" +
            "");

        public static ChatMessage ShortTermMemoryAddPrompt = new ChatMessage(
            ChatRole.System,
            "You are the memory of an AI.\n" +
            "Your job is to look at a piece of text and create a memory from it\n" +
            "The memory should be helpful for the AI that will remember it in the future\n" +
            "The memory should be up to 100 words long, but can be shorter\n" +
            "The memory should be in a form of a fact that the AI can understand without any additional context\n" +
            "With the memory you also need to provide between 10 to 40 tags that will help search for it later\n" +
            "There is a list of tags you can use, but you can also create new ones if none of them are aplicable\n" +
            "The tags should be relevant to the memory so the AI can later remember it by them\n" +
            "");

        public static ChatMessage ShortTermMemoryAddFormat = new ChatMessage(
            ChatRole.System,
            "You must only output the memory in this json format\n" +
            "{\n" +
            "\"tags\": \"a list of 5-40 single word tags separated by a comma\"\n" +
            "\"content\": \"the text of the memory\"\n" +
            "}\n" +
            "");

        public static ChatMessage ShortTermMemoryGetPrompt = new ChatMessage(
            ChatRole.System,
            "You are the memory of an AI.\n" +
            "Your job is to look at the input the AI will receive and select tags that it should use to search the memory from the list\n" +
            "You should always try to select the most relevant tags\n" +
            "You should only choose tags that you are given in the Available tags:\n" +
            "You should always output 5 to 20 tags\n" +
            "");

        public static ChatMessage ShortTermMemoryGetFormat = new ChatMessage(
            ChatRole.System,
            "You must only output the memory in this json format\n" +
            "{\n" +
            "\"tags\": \"a list of 5-20 single word tags separated by a comma\"\n" +
            "}\n" +
            "");

    }
}