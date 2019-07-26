namespace SubtaskAPI.Automapper
{
    public class TaskItemDTO
    {
        public int Id { get; protected set; }
        public int ParentId { get; protected set; }
        public string Title { get; protected set; }
        public bool Done { get; protected set; }
    }
}