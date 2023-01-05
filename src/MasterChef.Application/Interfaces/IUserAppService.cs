using MasterChef.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterChef.Application.Interfaces
{
    public interface IUserAppService
    {
        Task CreateNewUser(User newUser);
        Task<IList<User>> GetAll();
        Task<User> GetById(int id);
        Task<bool> IsValidUserAndPassword(User user);
        Task UpdateUser(User entity);
    }
}