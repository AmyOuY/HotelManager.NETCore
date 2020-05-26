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
    public class RoomData : IRoomData
    {
        private readonly ISqlDataAccess _sql;

        public RoomData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<RoomModel> GetRooms()
        {
            var output = _sql.LoadData<RoomModel, dynamic>("dbo.spRoom_GetAll", new { }, "OHMData");

            return output;
        }



        public RoomModel GetRoom(RoomModel room)
        {
            var output = _sql.LoadData<RoomModel, dynamic>("dbo.spRoomLookUp", new { room.RoomNumber }, "OHMData").FirstOrDefault();

            return output;
        }


        public int GetRoomID(RoomModel room)
        {
            var output = _sql.LoadData<int, dynamic>("dbo.spRoomIDLookUp", new { room.RoomNumber }, "OHMData").FirstOrDefault();

            return output;
        }


        public void SaveRoom(RoomModel room)
        {
            _sql.SaveData("dbo.spRoom_Insert", room, "OHMData");
        }


        public void UpdateRoom(RoomModel room)
        {
            _sql.SaveData("dbo.spRoom_Update", room, "OHMData");
        }


        public void DeleteRoom(int id)
        {
            _sql.DeleteData("dbo.spRoom_Remove", new { id }, "OHMData");
        }
    }
}
