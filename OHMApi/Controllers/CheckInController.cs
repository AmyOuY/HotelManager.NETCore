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
        private readonly ICheckInData _data;

        public CheckInController(ICheckInData data)
        {
            _data = data;
        }

        [HttpGet]
        public List<CheckInModel> Get()
        {
            return _data.GetCheckIns();
        }


        [Route("PostForCheckIn")]
        [HttpPost]
        public CheckInModel PostForCheckIn(ClientInfo cInfo)
        {
            return _data.GetCheckIn(cInfo);
        }



        [Route("PostForID")]
        [HttpPost]
        public int PostForCheckInID(CheckInModel checkIn)
        {
            return _data.GetCheckInID(checkIn);
        }


        [HttpPost]
        public void Post(CheckInModel checkIn)
        {
            _data.SaveCheckIn(checkIn);
        }


        [HttpPut]
        public void Put(int id, CheckInModel checkIn)
        {
            _data.UpdateCheckIn(checkIn);
        }


        [HttpDelete]
        public void Delete(int id)
        {
            _data.DeleteCheckIn(id);
        }
    }
}