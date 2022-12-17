using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MasterChef.Domain.Entities;

namespace MasterChef.Domain.Interface;

public interface IEventService
{
    Event Event { get; set; }
    Task<Event> Add(string name, List<ValidationFailure> messages);
}