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
    [Authorize(Roles = "Receptionist,Manager")]
    public class CheckOutController : ControllerBase
    {
        private readonly ICheckOutData _data;

        public CheckOutController(ICheckOutData data)
        {
            _data = data;
        }


        [HttpPost]
        public void Post(CheckOutModel checkOut)
        {
            _data.SaveCheckOut(checkOut);
        }
    }
}