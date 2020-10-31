using System;
using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Services.Specifications
{
    public class IsPrestationCategoryExistSpecification : BaseSpecification<Prestation>
    {
        public IsPrestationCategoryExistSpecification(string id = null, string name = null)
        : base(x =>
         (string.IsNullOrEmpty(id) || x.PrestationCategoryId == Int32.Parse(id)) &&
         (string.IsNullOrEmpty(name) || x.PrestationCategory.Name == name))
        {
        }
    }
}