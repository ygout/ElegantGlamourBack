using System;
using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Services.Specifications
{
    public class IsPrestationExistSpecification : BaseSpecification<Prestation>
    {
        public IsPrestationExistSpecification(string id = null, string title = null)
         : base(x =>
        (string.IsNullOrEmpty(id) || x.Id == Int32.Parse(id)) &&
        (string.IsNullOrEmpty(title) || x.Title == title)
         )
        {
        }
    }
}