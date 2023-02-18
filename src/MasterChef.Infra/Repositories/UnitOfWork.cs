using System.Threading.Tasks;
using MasterChef.Infra.Context;
using MasterChef.Infra.Interfaces;

namespace MasterChef.Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;

    public UnitOfWork(DatabaseContext context)
    {
        _context = context;     
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}