using MasterChef.Application.Interfaces;
using MasterChef.Domain.Entities;
using MasterChef.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Application.Services
{
    public class UserAppAppService : IUserAppService
    {
        private readonly IUserRepository _repository;

        public UserAppAppService(IUserRepository repository)
        {
            _repository = repository;
        }
        public void CreateNewUser(User newUser)
        {
            _repository.Add(newUser);

        }
        public async Task<IList<User>> GetAll()
        {
            var response = await _repository.GetAll();
            return response;
        }

        public async Task<User> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task UpdateUser(User entity)
        {
            await _repository.Update(entity);

        }
        public async Task<bool> IsValidUserAndPassword(User user)
        {
            var response = await _repository.GetByUserNameAndPassword(user);

            return response != null;
        }
    }
}
