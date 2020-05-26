using OHMDataManager.Library.Models;
using System.Collections.Generic;

namespace OHMDataManager.Library.DataAccess
{
    public interface ICheckInData
    {
        void DeleteCheckIn(int id);
        CheckInModel GetCheckIn(ClientInfo cInfo);
        int GetCheckInID(CheckInModel checkIn);
        List<CheckInModel> GetCheckIns();
        void SaveCheckIn(CheckInModel checkIn);
        void UpdateCheckIn(CheckInModel checkIn);
    }
}