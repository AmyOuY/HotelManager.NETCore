using OHMDataManager.Library.Models;
using System.Collections.Generic;

namespace OHMDataManager.Library.DataAccess
{
    public interface ISaleData
    {
        List<SaleReportModel> GetSaleReport();
    }
}