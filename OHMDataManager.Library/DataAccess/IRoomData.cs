using OHMDataManager.Library.Models;
using System.Collections.Generic;

namespace OHMDataManager.Library.DataAccess
{
    public interface IRoomData
    {
        void DeleteRoom(int id);
        RoomModel GetRoom(RoomModel room);
        int GetRoomID(RoomModel room);
        List<RoomModel> GetRooms();
        void SaveRoom(RoomModel room);
        void UpdateRoom(RoomModel room);
    }
}