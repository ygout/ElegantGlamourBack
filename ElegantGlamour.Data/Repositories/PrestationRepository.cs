using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Repositories;

namespace ElegantGlamour.Data.Repositories
{
    public class PrestationRepository : Repository<Prestation>, IPrestationRepository
    {
        public PrestationRepository(ElegantGlamourDbContext context) : base(context) {}
        
    }
}