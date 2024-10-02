using WPF_Learning.Core.Domains.Systems;
using WPF_Learning.Core.Repositories;
using WPF_Learning.Data.SeedWorks;
using WPF_Learning.Model;

namespace WPF_Learning.Data.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department, Guid>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
