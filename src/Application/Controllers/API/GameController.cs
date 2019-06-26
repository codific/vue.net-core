using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Game;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Controllers.API
{
    [Route("api/game")]
    public class GameController : Controller
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpPost]
        [Route("init")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> InitGame()
        {
            var game = await this.gameService.InitGameAsync();

            if (game == null)
            {
                return BadRequest();
            }

            return Ok(game);
        }

        [HttpPost]
        [Route("make-move/{gameId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> MakeMove([FromBody]MoveDto move, Guid gameId)
        {
            var game = await this.gameService.MakeMoveAsync(move, gameId);
            if (game == null)
            {
                return BadRequest();
            }
            return Ok(game);
        }
    }
}
