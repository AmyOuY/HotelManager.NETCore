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
    [Authorize(Roles = "Receptionist")]
    public class CheckInController : ControllerBase
    {
        private readonly IConfiguration _config;

        public CheckInController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public List<CheckInModel> Get()
        {
            CheckInData data = new CheckInData(_config);

            return data.GetCheckIns();
        }


        [Route("PostForCheckIn")]
        [HttpPost]
        public CheckInModel PostForCheckIn(ClientInfo cInfo)
        {
            CheckInData data = new CheckInData(_config);

            return data.GetCheckIn(cInfo);
        }



        [Route("PostForID")]
        [HttpPost]
        public int PostForCheckInID(CheckInModel checkIn)
        {
            CheckInData data = new CheckInData(_config);

            return data.GetCheckInID(checkIn);
        }


        [HttpPost]
        public void Post(CheckInModel checkIn)
        {
            CheckInData data = new CheckInData(_config);
            data.SaveCheckIn(checkIn);
        }


        [HttpPut]
        public void Put(int id, CheckInModel checkIn)
        {
            CheckInData data = new CheckInData(_config);
            data.UpdateCheckIn(checkIn);
        }


        [HttpDelete]
        public void Delete(int id)
        {
            CheckInData data = new CheckInData(_config);
            data.DeleteCheckIn(id);
        }
    }
}