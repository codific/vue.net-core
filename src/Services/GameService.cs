using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObjects.Game;
using Entities;
using Entities.Game;
using GSK.DAL;

namespace Services
{
    public class GameService : AbstractService, IGameService
    {
        private const string playerSign = "X";
        private const string machineSign = "O";

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

        public async Task<GameDto> MakeMoveAsync(MoveDto move, Guid gameId)
        {
            try
            {
                var standardRepository = this.uow.GetStandardRepository();
                Game currentGame = null;
                var mappedMove = this.mapper.Map<Move>(move);
                var moves = await standardRepository.QueryAsync<Move>(x => x.GameId == gameId);
                int nextOrder = 1;
                if (moves.Count() > 0)
                {
                    var maxOrder = moves.Select(x => x.Order).Max();
                    nextOrder = maxOrder + 1;
                }

                mappedMove.Order = nextOrder;
                mappedMove.IsPlayer = true;

                currentGame = await standardRepository.GetAsync<Game>(gameId);
                if (!currentGame.IsFinished)
                {
                    standardRepository.Add<Move>(mappedMove);
                    mappedMove.GameId = gameId;
                    await this.uow.SaveChangesAsync();
                }
                else
                {
                    return null;
                }

                if (moves.Select(x => (int)x.Position).ToList().Contains(move.Position))
                {
                    return null;
                }

                currentGame = await UpdateGameStatusAsync(gameId);
                if (!currentGame.IsFinished)
                {
                    currentGame = await MakeMachineTurnAsync(gameId);
                }
                
                return this.mapper.Map<GameDto>(currentGame);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<Game> MakeMachineTurnAsync(Guid gameId)
        {
            try
            {
                var standardRepository = this.uow.GetStandardRepository();
                var currentGame = await standardRepository.GetAsync<Game>(gameId);
                var moves = await standardRepository.QueryAsync<Move>(x => x.GameId == gameId);
                int nextOrder = 1;
                if (moves.Count() > 0)
                {
                    var maxOrder = moves.Select(x => x.Order).Max();
                    nextOrder = maxOrder + 1;
                }
                var movesInts = moves.Select(x => (int)x.Position).ToList();
                int machinePosition = GetFreeRandomPosition(movesInts);
                if (machinePosition < 0)
                {
                    return null;
                }

                var machineMove = new Move
                {
                    IsPlayer = false,
                    GameId = gameId,
                    Order = nextOrder,
                    Position = (MovePosition)machinePosition,
                };

                standardRepository.Add<Move>(machineMove);
                await this.uow.SaveChangesAsync();

                return await UpdateGameStatusAsync(gameId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private int GetFreeRandomPosition(List<int> currentPositions)
        {
            int counter = 0;
            for (int i = 1; i <= 9; i++)
            {
                if (currentPositions.Contains(i))
                {
                    counter++;
                }
            }
            if (counter == 9)
            {
                return -1;
            }

            Random random = new Random();
            int randomPosition = random.Next(1, 10);
            while (currentPositions.Contains(randomPosition))
            {
                randomPosition = random.Next(1, 10);
            }

            return randomPosition;
        }

        private async Task<Game> UpdateGameStatusAsync(Guid gameId)
        {
            try
            {
                var standardRepository = this.uow.GetStandardRepository();
                var currentGame = await standardRepository.GetAsync<Game>(gameId);
                var moves = await standardRepository.QueryAsync<Move>(x => x.GameId == gameId);
                var lastMove = moves.OrderBy(x => x.CreatedOn).LastOrDefault();
                var winningCombination = CheckForWinnerCombination(moves.ToList());
                if (winningCombination != null)
                {
                    currentGame.IsFinished = true;
                    currentGame.IsPlayerWin = lastMove.IsPlayer == true;
                    currentGame.WinningCombination = winningCombination;

                    standardRepository.Update<Game>(currentGame);
                    await this.uow.SaveChangesAsync();
                }

                return currentGame;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        private WinningCombination? CheckForWinnerCombination(List<Move> moves)
        {
            string[] gameboard = new string[9];
            foreach (var move in moves)
            {
                gameboard[(int)move.Position - 1] = move.IsPlayer ? playerSign : machineSign;
            }

            var winningCombination = GetGameResult(gameboard);
            if (winningCombination != null)
            {
                int winningCombinationInt = winningCombination[0] * 100 + winningCombination[1] * 10 + winningCombination[2];
                return (WinningCombination)winningCombinationInt;
            }

            return null;
        }

        public static int[] GetGameResult(string[] gameboard)
        {
            List<int[]> winnerCombination = new List<int[]>()
            {
                new int[3] { 0, 1, 2 },
                new int[3] { 3, 4, 5 },
                new int[3] { 6, 7, 8 },
                new int[3] { 0, 3, 6 },
                new int[3] { 1, 4, 7 },
                new int[3] { 2, 5, 8 },
                new int[3] { 0, 4, 8 },
                new int[3] { 2, 4, 6 }
            };

            foreach (var combination in winnerCombination)
            {
                if (gameboard[combination[0]] == gameboard[combination[1]] && gameboard[combination[1]] == gameboard[combination[2]] && gameboard[combination[0]] != null)
                {
                    return new int[3] { combination[0] + 1, combination[1] + 1, combination[2] + 1 };
                }
            }

            return null;
        }
    }
}
