using System;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Specifications;

namespace ElegantGlamour.Services.Specifications
{
    public class PrestationsCategorySpecification : BaseSpecification<PrestationCategory>
    {
        public PrestationsCategorySpecification(int id)
        : base(x => x.Id == id)
        {
        }

        public PrestationsCategorySpecification(PrestationCategorySpecParams spec)
        : base(x =>
       (!spec.Id.HasValue || x.Id == (spec.Id)) &&
       (string.IsNullOrEmpty(spec.Name) || x.Name == spec.Name)
        )
        {
            AddOrderBy(x => x.Name);
            if (!string.IsNullOrEmpty(spec.Sort))
            {
                switch (spec.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(p => p.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

        }
    }
}