using System.Collections.Generic;

namespace OHMDataManager.Library.Internal.DataAccess
{
    public interface ISqlDataAccess
    {
        void DeleteData<T>(string storedProcedure, T parameters, string connectionStringName);
        string GetConnectionString(string connectionStringName);
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        void SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
    }
}