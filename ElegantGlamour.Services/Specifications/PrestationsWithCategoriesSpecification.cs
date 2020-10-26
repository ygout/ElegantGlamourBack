using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Specifications;

namespace ElegantGlamour.Services.Specifications
{
    public class PrestationsWithCategoriesSpecification : BaseSpecification<Prestation>
    {
        public PrestationsWithCategoriesSpecification(PrestationSpecParams prestationSpecParams)
        : base(x =>
            (string.IsNullOrEmpty(prestationSpecParams.Search) || x.Title.ToLower().Contains(prestationSpecParams.Search)) &&
            (!prestationSpecParams.CategoryId.HasValue || x.PrestationCategoryId == prestationSpecParams.CategoryId)
            )
        {
            AddInclude(x => x.PrestationCategory);
            AddOrderBy(x => x.Title);
            if (!string.IsNullOrEmpty(prestationSpecParams.Sort))
            {
                switch (prestationSpecParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Title);
                        break;
                }
            }
        }
        public PrestationsWithCategoriesSpecification(int id)
        : base(x => x.Id == id)
        {
            AddInclude(x => x.PrestationCategory);
        }
    }
}