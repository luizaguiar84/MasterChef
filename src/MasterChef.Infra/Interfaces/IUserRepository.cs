using MasterChef.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterChef.Infra.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<IList<User>> GetAll();
        Task<User> GetById(int id);
        Task Update(User entity);
        Task<User> GetByUserNameAndPassword(User user);

    }
}