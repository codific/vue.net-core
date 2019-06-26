using DataTransferObjects.Game;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IGameService
    {
        Task<GameDto> InitGameAsync();

        Task<GameDto> GetCurrentGameAsync();

        Task<GameDto> MakeMoveAsync(MoveDto move, Guid gameId);

        Task<IEnumerable<MoveDto>> GetAllMovesPerGameAsync(Guid gameId);
    }
}
