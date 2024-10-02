using WPF_Learning.Core.Repositories;

namespace WPF_Learning.Core.SeedWorks
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        Task<int> CompleteAsync();
    }
}
