using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OHMDataManager.Library.DataAccess;
using OHMDataManager.Library.Models;

namespace OHMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleData _data;

        public SaleController(ISaleData data)
        {
            _data = data;
        }


        [Route("GetSaleReport")]
        [HttpGet]
        public List<SaleReportModel> Get()
        {
            return _data.GetSaleReport();
        }
    }
}