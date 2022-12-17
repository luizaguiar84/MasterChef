using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Message { get; set; }
        public List<string>? EventsList { get; }
        public DateTime? DateTimeEvent { get; }

        public Event()
        {
            Id = Guid.NewGuid();
            DateTimeEvent = DateTime.Now;
            EventsList = new List<string>();
        }
    }
}
