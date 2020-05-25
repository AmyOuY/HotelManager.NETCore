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
    [Authorize(Roles = "Manager,Receptionist")]
    public class RoomController : ControllerBase
    {
        private readonly IConfiguration _config;

        public RoomController(IConfiguration config)
        {
            _config = config;
        }


        [HttpGet]
        public List<RoomModel> Get()
        {
            RoomData data = new RoomData(_config);

            return data.GetRooms();
        }


        [Route("PostForRoom")]
        [HttpPost]
        public RoomModel PostForRoom(RoomModel room)
        {
            RoomData data = new RoomData(_config);

            return data.GetRoom(room);
        }


        [Route("PostForID")]
        [HttpPost]
        public int PostForRoomID(RoomModel room)
        {
            RoomData data = new RoomData(_config);

            return data.GetRoomID(room);
        }


        [HttpPost]
        public void Post(RoomModel room)
        {
            RoomData data = new RoomData(_config);
            data.SaveRoom(room);
        }


        [HttpPut]
        public void Put(int id, RoomModel room)
        {
            RoomData data = new RoomData(_config);
            data.UpdateRoom(room);
        }


        [HttpDelete]
        public void Delete(int id)
        {
            RoomData data = new RoomData(_config);
            data.DeleteRoom(id);
        }
    }
}