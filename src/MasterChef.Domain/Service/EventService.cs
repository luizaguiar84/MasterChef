using MasterChef.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MasterChef.Domain.Interface;

namespace MasterChef.Domain.Service
{
    public class EventService : IEventService
    {
        public Event Event { get; set; }

        public EventService()
        {
            Event = new Event();
        }

        public Task<Event> Add(string name, List<ValidationFailure> messages)
        {
            var _event = new Event() { Name = name };

            foreach (var item in messages)
            {
                Event.EventsList?.Add(item.ErrorMessage);
            }

            return Task.FromResult(Event);
        }
    }
}
