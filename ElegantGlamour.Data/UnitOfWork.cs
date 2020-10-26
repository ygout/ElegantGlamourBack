using System.Threading.Tasks;
using ElegantGlamour.Core;
using ElegantGlamour.Core.Repositories;
using ElegantGlamour.Data.Repositories;

namespace ElegantGlamour.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ElegantGlamourDbContext _context;
        private PrestationRepository _prestationRepository;

        public UnitOfWork(ElegantGlamourDbContext context)
        {
            this._context = context;
        }
        public IPrestationRepository Prestations => _prestationRepository = _prestationRepository ?? new PrestationRepository(_context);

        public IHomePageRepository HomePages => throw new System.NotImplementedException();

        public ISocialLinkRepository SocialLinks => throw new System.NotImplementedException();

        public IPortfolioImageRepository PortfolioImages => throw new System.NotImplementedException();

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}