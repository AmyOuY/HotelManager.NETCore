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
    public class CheckOutData
    {
        private readonly IConfiguration _config;

        public CheckOutData(IConfiguration config)
        {
            _config = config;
        }

        public void SaveCheckOut(CheckOutModel checkOut)
        {
            SqlDataAccess data = new SqlDataAccess(_config);

            data.SaveData("dbo.spCheckOut_Insert", checkOut, "OHMData");
        }
    }
}
