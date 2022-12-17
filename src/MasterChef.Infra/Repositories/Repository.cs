using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterChef.Infra.Context;
using MasterChef.Infra.Interfaces;

namespace MasterChef.Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _databaseContext;
        protected DbSet<T> DbSet;

        public Repository(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
            DbSet = databaseContext.Set<T>();
        }
        public async Task<T> Save(T entity)
        {
            var response = DbSet.Add(entity) as T;
            await _databaseContext.SaveChangesAsync();
            return response;
        }
        public async Task<T> Update(T entity)
        {

            var entry = _databaseContext.Entry(entity);
            DbSet.Attach(entity);
            entry.State = EntityState.Modified;
            await _databaseContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var response = DbSet.Remove(await DbSet.FindAsync(id));
            
            if (response != null)
            {
                await _databaseContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }

}
