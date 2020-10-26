using System;
using System.Collections.Generic;

namespace ElegantGlamour.Core.Models
{
    public class Appointment : BaseEntity
    {
        public List<Prestation> Prestations { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}