namespace WPF_Learning.Core.Domains.Systems
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid UserId { get; set; }
    }
}
