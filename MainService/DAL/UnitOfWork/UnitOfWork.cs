using DAL.Contracts.UnitOfWork;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainServiceContext _context;

        public UnitOfWork(MainServiceContext context)
        {
            _context = context;
        }
        public Task<int> SaveChanges(CancellationToken token)
        {
            return _context.SaveChangesAsync(token);
        }
    }
}
