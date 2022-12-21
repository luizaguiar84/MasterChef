using System.Threading.Tasks;

namespace MasterChef.Infra.Interfaces;

public interface IUnitOfWork
{
    Task CompleteAsync();
}