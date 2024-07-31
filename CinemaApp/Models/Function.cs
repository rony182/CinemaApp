using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Models
{
    public class Function
    {
        public DateTime Date { get; set; }
        public TimeSpan ScheduleHour { get; set; }
        public decimal Price { get; set; }
        public string MovieName { get; set; }
        public string DirectorName { get; set; }
        public bool IsInternational { get; set; }
    }
}
