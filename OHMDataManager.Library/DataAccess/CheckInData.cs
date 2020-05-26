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
    public class CheckInData : ICheckInData
    {
        private readonly ISqlDataAccess _sql;

        public CheckInData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<CheckInModel> GetCheckIns()
        {
            var output = _sql.LoadData<CheckInModel, dynamic>("dbo.spCheckIn_GetAll", new { }, "OHMData");

            return output;
        }


        public CheckInModel GetCheckIn(ClientInfo cInfo)
        {
            var output = _sql.LoadData<CheckInModel, dynamic>("dbo.spCheckInLookUp", new { cInfo.Client, cInfo.Phone }, "OHMData").FirstOrDefault();

            return output;
        }


        public int GetCheckInID(CheckInModel checkIn)
        {
            var output = _sql.LoadData<int, dynamic>("dbo.spCheckInIDLookUp", new { checkIn.Client, checkIn.Phone }, "OHMData").FirstOrDefault();

            return output;
        }


        public void SaveCheckIn(CheckInModel checkIn)
        {
            _sql.SaveData("dbo.spCheckIn_Insert", checkIn, "OHMData");
        }


        public void UpdateCheckIn(CheckInModel checkIn)
        {
            _sql.SaveData("dbo.spCheckIn_Update", checkIn, "OHMData");
        }


        public void DeleteCheckIn(int id)
        {
            _sql.DeleteData("dbo.spCheckIn_Remove", new { id }, "OHMData");
        }
    }
}
