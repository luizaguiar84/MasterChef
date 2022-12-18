using System;
using System.Collections.Generic;

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
