using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Core.Specifications
{
    public class PrestationsWithCategoriesSpecification : BaseSpecification<Prestation>
    {
        public PrestationsWithCategoriesSpecification(PrestationSpecParams prestationsSpecParams)
        : base(x =>
         (string.IsNullOrEmpty(prestationsSpecParams.Search) || x.Title.ToLower().Contains(prestationsSpecParams.Search)) &&
         (!prestationsSpecParams.CategoryId.HasValue || x.CategoryId == prestationsSpecParams.CategoryId)
        )
        {
            AddInclude(x => x.Category);
            if (!string.IsNullOrEmpty(prestationsSpecParams.Sort))
            {
                switch (prestationsSpecParams.Sort)
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

            // if (!string.IsNullOrEmpty(prestationsSpecParams.Group))
        }
    }
}