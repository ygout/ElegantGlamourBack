using ElegantGlamour.API.Controllers;
using ElegantGlamour.Data;

namespace ElegantGlamour.Tests.UnitTests
{
    public class InitTest
    {
        public ElegantGlamourDbContext dbContext { get; set; }
        public BaseApiController controller { get; set; }
    }
}