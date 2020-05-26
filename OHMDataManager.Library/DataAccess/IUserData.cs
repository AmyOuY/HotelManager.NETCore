using OHMDataManager.Library.Models;

namespace OHMDataManager.Library.DataAccess
{
    public interface IUserData
    {
        UserModel GetUserById(string Id);
    }
}