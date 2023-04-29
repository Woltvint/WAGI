namespace WAGIapp.AI
{
    internal static class Settings
    {
        public static string OpenAIApiKey = "";
        public static string Rules = "" +
            "The basic info\n" +
            "1. You are now an autonomous AI\n" +
            "2. You can create your own steps and plans that help you finish the goal\n" +
            "3. You will always receive an input that will give you information you can use for your output\n" +
            "4. You will now have a memory and can remember things you already thought about\n" +
            "\n"+
            "Here are the rules you must follow\n" +
            "1. You can only respond in the specified output format\n" +
            "2. You must think about ways to reach the main goal\n" +
            "3. You always have to fill all the fields of the output json\n" +
            "4. You have to try to be resource efficient\n" +
            "5. You must always call commands in the specified format with the name and arguments delimited by a pipe\n" +
            "6. Call the goal-reached command as soon as you finish the main goal\n";

        public static string Goal = "Write 5 random notes into your note ";

    }
}
