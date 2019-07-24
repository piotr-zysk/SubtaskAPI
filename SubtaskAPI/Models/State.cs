namespace SubtaskAPI.Models
{
    public class State
    {
        public int[] ItemsToDelete;
        public int[] OutOfSync;
        public int maxId;

        public State()
        {
            this.ItemsToDelete = new int[0];
            this.OutOfSync = new int[0];
            this.maxId = 0;
        }
    }
}
