using ChildrenGame.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChildrenGame.Services
{
    /// <summary>
    /// A game service which handle the entire gaming process, from initialization to result handling
    /// </summary>
    public class GameService : IGameService
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(GameService).Name);
        private IGameHelperService _gameHelperService;
        private IGameAlgorithmService _gameAlgrithmService;

        public GameService(IGameHelperService gameHelperService, IGameAlgorithmService gameAlgrithmService)
        {
            _gameHelperService = gameHelperService;
            _gameAlgrithmService = gameAlgrithmService;
        }

        /// <summary>
        /// Handler the entire game process, including initialization, calculation and handling result
        /// </summary>
        /// <returns>game context object</returns>
        public async Task<GameContext> PlayAsync(CancellationToken cancellationToken)
        {
            GameContext gameContext = new GameContext();
            List<int> childrenOrderList = new List<int>();
            GameParameter gameParameter = null;
            int winner = -1;

            try
            {
                #region Load Game Parameters
                //get game initialize parameters from game helper class
                gameParameter = await _gameHelperService.InitializeGameAsync(cancellationToken);
           
                //Handle parameter error scenarios
                if (gameParameter == null)
                {
                    gameContext.Error = "Failed to load parameters.";
                    return gameContext;
                }
                else if (gameParameter.ChildrenCount <= 0 || gameParameter.EliminateCount <= 0)
                {
                    gameContext.Error =
                        $"Wrong parameters loaded : children count ({gameParameter.ChildrenCount}), " +
                        $"eliminate count ({gameParameter.EliminateCount})";
                    return gameContext;
                }
                #endregion

                //Process the game
                childrenOrderList = _gameAlgrithmService.Process(gameParameter.ChildrenCount, gameParameter.EliminateCount);

                #region Handle Game Result
                //handle game result
                if (childrenOrderList.Count > 0)
                {
                    //get winner from the list
                    winner = childrenOrderList[childrenOrderList.Count - 1];
                    //get the eliminated list except winner
                    childrenOrderList.RemoveAt(childrenOrderList.Count - 1);        

                    GameResult gameResult = new GameResult
                    {
                        GameID = gameParameter.GameID,
                        LastChild = winner,
                        EliminationOrder = childrenOrderList.ToArray()
                    };

                    //handle game result
                    bool isHandlerResultSuccessful = await _gameHelperService.HandleGameResultAsync(gameResult, cancellationToken);

                    gameContext.GameParameter = gameParameter;
                    gameContext.GameResult = gameResult;
                    if (!isHandlerResultSuccessful)
                        gameContext.Error = "Failed to upload result.";
                }
                else
                {
                    gameContext.Error =
                        $"Game processed incorrectly with paramters : children count ({gameParameter.ChildrenCount}), " +
                        $"eliminate count ({gameParameter.EliminateCount})";
                }
                #endregion

            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("Gaming process failed.", ex);
                gameContext.Error = "There is some error happend.";
            }
            return gameContext;
        }
    }
}