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
    public class ClientController : ControllerBase
    {
        private readonly IClientData _data;

        public ClientController(IClientData data)
        {
            _data = data;
        }


        [HttpGet]
        public List<ClientModel> Get()
        {
            return _data.GetClients();
        }


        [Route("PostForID")]
        [HttpPost]
        public int PostForClientID(ClientModel client)
        {
            return _data.GetClientID(client);
        }


        [HttpPost]
        public void Post(ClientModel client)
        {
            _data.SaveClient(client);
        }


        [HttpPut]
        public void Put(int id, ClientModel client)
        {
            _data.UpdateClient(client);
        }


        [HttpDelete]
        public void Delete(int id)
        {
            _data.DeleteClient(id);
        }
    }
}