using System.Threading.Tasks;

namespace CanedoLab.MS.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
