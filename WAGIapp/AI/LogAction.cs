using MudBlazor;

namespace WAGIapp.AI
{
    public class LogAction
    {
        public string Title { get; set; } = "";
        public string Icon { get; set; } = "";
        public string Text { get; set; } = "";
        public bool Last { get; set; } = false;

        public const string InfoIcon = Icons.Material.Filled.Info;
        public const string MemoryIcon = Icons.Material.Filled.Memory;
        public const string ThinkIcon = Icons.Material.Filled.Psychology;
        public const string CommandIcon = Icons.Material.Filled.Terminal;
    }
}
