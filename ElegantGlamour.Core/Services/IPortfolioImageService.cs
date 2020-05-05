using System.Collections.Generic;
using System.Threading.Tasks;
using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Core.Services
{
    public interface IPortfolioImageService
    {
        Task<IEnumerable<PortfolioImage>> GetAllPrestations();
        Task<PortfolioImage> GetPrestationById(int id);
        Task<PortfolioImage> CreatePrestation(PortfolioImage newArtist);
        Task UpdatePrestation(PortfolioImage portfolioImage);
        Task DeletePrestation(PortfolioImage portfolio);
    }
}