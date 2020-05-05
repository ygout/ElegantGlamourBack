using System;
using System.Collections.Generic;

namespace ElegantGlamour.Core.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public List<Prestation> Prestations { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}