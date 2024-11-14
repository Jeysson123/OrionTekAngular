using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.Config;
using Models.Entities;
using Services.Services;
using Services.Utils;

namespace Api
{
    /*Api gateway that connect the views with services*/
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<WebResponse<IEnumerable<Client>>>> GetClients()
        {
            var clients = await Task.FromResult(_clientService.GetAllClients()).Result;

            return Ok(new WebResponse<IEnumerable<Client>>
            {
                Message = await Task.FromResult(HttpStatusUtils.GetStatus().Result.CurrentMsg),
                IsSuccess = await Task.FromResult(HttpStatusUtils.GetStatus().Result.CurrentStatus),
                StatusCode = await Task.FromResult(HttpStatusUtils.GetStatus().Result.CurrentCode),
                Body = clients
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WebResponse<Client>>> GetClient(int id)
        {
            var client = await Task.FromResult(_clientService.GetUniqueClient(id)).Result;

            return Ok(new WebResponse<Client>
            {
                Message = await Task.FromResult(HttpStatusUtils.GetStatus().Result.CurrentMsg),
                IsSuccess = await Task.FromResult(HttpStatusUtils.GetStatus().Result.CurrentStatus),
                StatusCode = await Task.FromResult(HttpStatusUtils.GetStatus().Result.CurrentCode),
                Body = client
            });
        }

        [HttpPost]
        public async Task<ActionResult<WebResponse<bool>>> PostClient([FromBody] Client client)
        {
            var clientInserted = await Task.FromResult(_clientService.AddClient(client)).Result;

            return Ok(new WebResponse<bool>
            {
                Message = await Task.FromResult(HttpStatusUtils.GetStatus().Result.CurrentMsg),
                IsSuccess = await Task.FromResult(HttpStatusUtils.GetStatus().Result.CurrentStatus),
                StatusCode = await Task.FromResult(HttpStatusUtils.GetStatus().Result.CurrentCode),
                Body = clientInserted
            });
        }

        [HttpPut]
        public async Task<ActionResult<WebResponse<bool>>> ModifyClient([FromBody] Client client)
        {
            var clientUpdated = await Task.FromResult(_clientService.UpdateClient(client)).Result;

            return Ok(new WebResponse<bool>
            {
                Message = await Task.FromResult(HttpStatusUtils.GetStatus().Result.CurrentMsg),
                IsSuccess = await Task.FromResult(HttpStatusUtils.GetStatus().Result.CurrentStatus),
                StatusCode = await Task.FromResult(HttpStatusUtils.GetStatus().Result.CurrentCode),
                Body = clientUpdated
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<WebResponse<bool>>> RemoveClient(int id)
        {
            var clientDeleted = await Task.FromResult(_clientService.DeleteClient(id)).Result;

            return Ok(new WebResponse<bool>
            {
                Message = await Task.FromResult(HttpStatusUtils.GetStatus().Result.CurrentMsg),
                IsSuccess = await Task.FromResult(HttpStatusUtils.GetStatus().Result.CurrentStatus),
                StatusCode = await Task.FromResult(HttpStatusUtils.GetStatus().Result.CurrentCode),
                Body = clientDeleted
            });
        }
    }
}
