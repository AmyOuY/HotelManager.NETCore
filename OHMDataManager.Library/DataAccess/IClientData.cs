using OHMDataManager.Library.Models;
using System.Collections.Generic;

namespace OHMDataManager.Library.DataAccess
{
    public interface IClientData
    {
        void DeleteClient(int id);
        int GetClientID(ClientModel client);
        List<ClientModel> GetClients();
        void SaveClient(ClientModel client);
        void UpdateClient(ClientModel client);
    }
}