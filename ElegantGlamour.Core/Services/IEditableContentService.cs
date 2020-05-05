using System.Threading.Tasks;
using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Core.Services
{
    public interface IEditableContentService
    {
        Task<HomePage> GetHomePageContent(int id = 1);
        Task<Footer> GetFooterContent(int id = 1);
    }
}