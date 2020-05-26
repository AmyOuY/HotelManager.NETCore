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
    public class CheckOutData : ICheckOutData
    {
        private readonly ISqlDataAccess _sql;

        public CheckOutData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public void SaveCheckOut(CheckOutModel checkOut)
        {
            _sql.SaveData("dbo.spCheckOut_Insert", checkOut, "OHMData");
        }
    }
}
