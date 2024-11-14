using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Config;
using Models.Entities;
using Services.Services;
using Services.Utils;

namespace Api
{
    [Route("api/direction")]
    [ApiController]
    public class DirectionController : ControllerBase
    {
        private readonly DirectionService _directionService;

        public DirectionController(DirectionService directionService)
        {
            _directionService = directionService;
        }

        [HttpGet]
        public async Task<ActionResult<WebResponse<IEnumerable<Direction>>>> GetDirections()
        {
            var directions = await _directionService.GetAllDirections();

            return Ok(new WebResponse<IEnumerable<Direction>>
            {
                Message = HttpStatusUtils.GetStatus().Result.CurrentMsg,
                IsSuccess = HttpStatusUtils.GetStatus().Result.CurrentStatus,
                StatusCode = HttpStatusUtils.GetStatus().Result.CurrentCode,
                Body = directions
            });
        }

        [HttpGet("byclientid")]
        public async Task<ActionResult<WebResponse<IEnumerable<Direction>>>> GetDirectionsByClient(int id)
        {
            var directions = await _directionService.GetAllDirectionsByClientId(id);

            return Ok(new WebResponse<IEnumerable<Direction>>
            {
                Message = HttpStatusUtils.GetStatus().Result.CurrentMsg,
                IsSuccess = HttpStatusUtils.GetStatus().Result.CurrentStatus,
                StatusCode = HttpStatusUtils.GetStatus().Result.CurrentCode,
                Body = directions
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WebResponse<Direction>>> GetDirection(int id)
        {
            var direction = await _directionService.GetUniqueDirection(id);

            return Ok(new WebResponse<Direction>
            {
                Message = HttpStatusUtils.GetStatus().Result.CurrentMsg,
                IsSuccess = HttpStatusUtils.GetStatus().Result.CurrentStatus,
                StatusCode = HttpStatusUtils.GetStatus().Result.CurrentCode,
                Body = direction
            });
        }

        [HttpPost]
        public async Task<ActionResult<WebResponse<bool>>> PostDirection([FromBody] Direction direction)
        {
            var directionInserted = await _directionService.AddDirection(direction);

            return Ok(new WebResponse<bool>
            {
                Message = HttpStatusUtils.GetStatus().Result.CurrentMsg,
                IsSuccess = HttpStatusUtils.GetStatus().Result.CurrentStatus,
                StatusCode = HttpStatusUtils.GetStatus().Result.CurrentCode,
                Body = directionInserted
            });
        }

        [HttpPut]
        public async Task<ActionResult<WebResponse<bool>>> ModifyDirection([FromBody] Direction direction)
        {
            var directionUpdated = await _directionService.UpdateDirection(direction);

            return Ok(new WebResponse<bool>
            {
                Message = HttpStatusUtils.GetStatus().Result.CurrentMsg,
                IsSuccess = HttpStatusUtils.GetStatus().Result.CurrentStatus,
                StatusCode = HttpStatusUtils.GetStatus().Result.CurrentCode,
                Body = directionUpdated
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<WebResponse<bool>>> RemoveDirection(int id)
        {
            var directionDeleted = await _directionService.DeleteDirection(id);

            return Ok(new WebResponse<bool>
            {
                Message = HttpStatusUtils.GetStatus().Result.CurrentMsg,
                IsSuccess = HttpStatusUtils.GetStatus().Result.CurrentStatus,
                StatusCode = HttpStatusUtils.GetStatus().Result.CurrentCode,
                Body = directionDeleted
            });
        }
    }
}
