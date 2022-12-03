using System.Collections.Generic;
using System.Threading.Tasks;
using MasterChef.Domain.Entities;

namespace MasterChef.Infra.Repositories.Interfaces
{
	public interface ILeadRepository
	{
		Task<Lead> GetById(int id);
		Task<IEnumerable<Lead>> GetAll();
		Task<int> CreateNew(Lead lead);
		Task Delete(int id);
		bool Exists(int id);
	}
}