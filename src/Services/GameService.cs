using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.Game;
using Entities.Game;
using GSK.DAL;

namespace Services
{
    public class GameService : AbstractService, IGameService
    {
        private const string playerSign = "X";

        public GameService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {

        }

        public async Task<GameDto> GetCurrentGameAsync()
        {
            try
            {
                var standardRepository = this.uow.GetStandardRepository();
                var currentGame = (await standardRepository.QueryAsync<Game>(x => !x.IsFinished)).FirstOrDefault();

                var currentGameDto = this.mapper.Map<GameDto>(currentGame);

                return currentGameDto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<GameDto> InitGameAsync()
        {
            try
            {
                var currentGame = await GetCurrentGameAsync();
                if (currentGame == null)
                {
                    var standardRepository = this.uow.GetStandardRepository();
                    var game = new Game
                    {
                        PlayerSign = playerSign,
                        IsFinished = false
                    };

                    standardRepository.Add<Game>(game);

                    await this.uow.SaveChangesAsync();

                    currentGame = await GetCurrentGameAsync();
                }

                return currentGame;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<GameDto> MakeMoveAsync(MoveDto move)
        {
            return null;
        }
    }
}
