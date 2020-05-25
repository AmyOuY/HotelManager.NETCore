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
        private readonly IConfiguration _config;

        public ClientController(IConfiguration config)
        {
            _config = config;
        }


        [HttpGet]
        public List<ClientModel> Get()
        {
            ClientData data = new ClientData(_config);

            return data.GetClients();
        }


        [Route("PostForID")]
        [HttpPost]
        public int PostForClientID(ClientModel client)
        {
            ClientData data = new ClientData(_config);

            return data.GetClientID(client);
        }


        [HttpPost]
        public void Post(ClientModel client)
        {
            ClientData data = new ClientData(_config);
            data.SaveClient(client);
        }


        [HttpPut]
        public void Put(int id, ClientModel client)
        {
            ClientData data = new ClientData(_config);
            data.UpdateClient(client);
        }


        [HttpDelete]
        public void Delete(int id)
        {
            ClientData data = new ClientData(_config);
            data.DeleteClient(id);
        }
    }
}