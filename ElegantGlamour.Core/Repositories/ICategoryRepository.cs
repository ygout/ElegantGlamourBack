using System;
using System.Threading.Tasks;
using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
         Task<Boolean> IsCategoryIdExist(int id);
    }
}