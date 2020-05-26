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
        private readonly IRoomData _data;

        public RoomController(IRoomData data)
        {
            _data = data;
        }


        [HttpGet]
        public List<RoomModel> Get()
        {
            return _data.GetRooms();
        }


        [Route("PostForRoom")]
        [HttpPost]
        public RoomModel PostForRoom(RoomModel room)
        {
            return _data.GetRoom(room);
        }


        [Route("PostForID")]
        [HttpPost]
        public int PostForRoomID(RoomModel room)
        {
            return _data.GetRoomID(room);
        }


        [HttpPost]
        public void Post(RoomModel room)
        {
            _data.SaveRoom(room);
        }


        [HttpPut]
        public void Put(int id, RoomModel room)
        {
            _data.UpdateRoom(room);
        }


        [HttpDelete]
        public void Delete(int id)
        {
            _data.DeleteRoom(id);
        }
    }
}