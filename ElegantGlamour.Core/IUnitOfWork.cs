using System;
using System.Threading.Tasks;
using ElegantGlamour.Core.Repositories;

namespace ElegantGlamour.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IPrestationRepository Prestations { get; }
        IHomePageRepository HomePages { get; }
        ISocialLinkRepository SocialLinks { get; }
        IPortfolioImageRepository PortfolioImages { get; }
        Task<int> CommitAsync();
    }
}