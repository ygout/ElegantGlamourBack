using System;
using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Services.Specifications
{
    public class PrestationsCategorySpecification : BaseSpecification<PrestationCategory>
    {
        public PrestationsCategorySpecification(string id = null, string name = null)
        : base(x =>
       (string.IsNullOrEmpty(id) || x.Id == Int32.Parse(id)) &&
       (string.IsNullOrEmpty(name) || x.Name == name)
        )
        {
        }
    }
}