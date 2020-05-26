using Microsoft.Extensions.Configuration;
using OHMDataManager.Library.Internal.DataAccess;
using OHMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDataManager.Library.DataAccess
{
    public class ClientData : IClientData
    {
        private readonly ISqlDataAccess _sql;

        public ClientData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<ClientModel> GetClients()
        {
            var output = _sql.LoadData<ClientModel, dynamic>("dbo.spClient_GetAll", new { }, "OHMData");

            return output;
        }


        public int GetClientID(ClientModel client)
        {
            var output = _sql.LoadData<int, dynamic>("dbo.spClientIDLookUp", new { client.FirstName, client.LastName, client.Phone }, "OHMData").FirstOrDefault();

            return output;
        }


        public void SaveClient(ClientModel client)
        {
            _sql.SaveData("dbo.spClient_Insert", client, "OHMData");
        }


        public void UpdateClient(ClientModel client)
        {
            _sql.SaveData("dbo.spClient_Update", client, "OHMData");
        }


        public void DeleteClient(int id)
        {
            _sql.DeleteData("dbo.spClient_Remove", new { id }, "OHMData");
        }
    }
}
