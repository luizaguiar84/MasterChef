using MasterChef.Domain.Entities;
using MasterChef.Infra.Context;
using MasterChef.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            user.CreateDate = DateTime.Now;
            user.LastChange = DateTime.Now;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

        }

        public async Task<IList<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(r => r.Id == id);
            return user;
        }

        public async Task Update(User entity)
        {
            entity.LastChange = DateTime.Now;
            _context.Users.Update(entity);

            _context.Entry(entity).Property(p => p.CreateDate).IsModified = false;

            await _context.SaveChangesAsync();
        }
        public async Task<User> GetByUserNameAndPassword(User user)
        {
            var response = _context.Users.FirstOrDefault(u => u.Username.Equals(user.Username) && u.Password.Equals(user.Password));

            return response;
        }
    }
}
