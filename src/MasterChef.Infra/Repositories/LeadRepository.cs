using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterChef.Domain.Entities;
using MasterChef.Infra.Context;
using MasterChef.Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Infra.Repositories
{
    public class LeadRepository : ILeadRepository
    {
        private readonly CrudDbContext _context;

        public LeadRepository(CrudDbContext context)
        {
            this._context = context;
        }

        public async Task<Lead> GetById(int id)
        {
            Lead lead = await _context.Leads
                .FirstOrDefaultAsync(m => m.Id == id);

            return lead;
        }

        public async Task<IEnumerable<Lead>> GetAll()
        {
            var leads = await _context.Leads.ToListAsync();
            return leads;
        }

        public async Task<int> CreateNew(Lead lead)
        {
            lead.DataCadastro = DateTime.Now;
            lead.LastUpdate = DateTime.Now;

            var entity = _context.Add(lead);
            await _context.SaveChangesAsync();

            var id = entity.Entity.Id;
            return id;
        }

        public async Task Delete(int id)
        {
            var lead = await GetById(id);

            if (lead == null)
                return;

            _context.Leads.Remove(lead);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            var exists = _context.Leads.Any(e => e.Id == id);
            return exists;
        }
    }
}