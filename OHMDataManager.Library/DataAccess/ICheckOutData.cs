using OHMDataManager.Library.Models;

namespace OHMDataManager.Library.DataAccess
{
    public interface ICheckOutData
    {
        void SaveCheckOut(CheckOutModel checkOut);
    }
}