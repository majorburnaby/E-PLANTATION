using Plantation.Models.DB;

namespace Plantation.Repository.Interface
{
    public interface ILoginRepository
    {
        Login FindByUsername(string username);
    }
}