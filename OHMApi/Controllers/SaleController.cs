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
        private readonly IConfiguration _config;

        public SaleController(IConfiguration config)
        {
            _config = config;
        }


        [Route("GetSaleReport")]
        [HttpGet]
        public List<SaleReportModel> Get()
        {
            SaleData data = new SaleData(_config);

            return data.GetSaleReport();
        }
    }
}