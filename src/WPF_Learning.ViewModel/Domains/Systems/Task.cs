namespace WPF_Learning.Core.Domains.Systems
{
    public class Task
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
    }
}
