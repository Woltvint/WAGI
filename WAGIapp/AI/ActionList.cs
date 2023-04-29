namespace WAGIapp.AI
{
    public class ActionList
    {
        private readonly object dataLock = new object(); 

        private List<LogAction> Actions;
        private int MaxActions;

        public ActionList(int maxActions) 
        {
            Actions = new List<LogAction>();
            MaxActions = maxActions;
        }

        public LogAction AddAction(string action, string icon = LogAction.InfoIcon)
        {
            LogAction a = new LogAction() { Title = action, Icon = icon };

            lock (dataLock)
            {
                Actions.Add(a);

                if (Actions.Count >= MaxActions)
                    Actions.RemoveAt(0);
            }

            return a;
        }

        public List<LogAction> GetActions()
        {
            foreach (var action in Actions)
            {
                action.Last = false;
            }

            if (Actions.Count > 0)
                Actions.Last().Last = true;

            return Actions.ToList();
        }
    }
}
