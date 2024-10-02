using WPF_Learning.Core.Repositories;
using WPF_Learning.Core.SeedWorks;
using WPF_Learning.Data.Repositories;
using WPF_Learning.Model;

namespace WPF_Learning.Data.SeedWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            DepartmentRepository = new DepartmentRepository(context);
        }

        public IDepartmentRepository DepartmentRepository { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
