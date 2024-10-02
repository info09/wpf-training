using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Learning.Core.Domains.Systems;
using WPF_Learning.Core.SeedWorks;

namespace WPF_Learning.Core.Repositories
{
    public interface IDepartmentRepository : IRepositoryBase<Department, Guid>
    {
    }
}
