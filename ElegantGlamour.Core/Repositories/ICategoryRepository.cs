using System;
using System.Threading.Tasks;
using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> IsCategoryIdExist(int id);
        Task<bool> IsCategoryTitleExist(string title);
    }
}