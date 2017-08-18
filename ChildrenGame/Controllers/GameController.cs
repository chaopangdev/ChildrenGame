using ChildrenGame.Models;
using ChildrenGame.Services;
using log4net;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChildrenGame.Controllers
{
    public class GameController : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(GameController).Name);

        private IGameService _gameService;

        public GameController()
        {
            _gameService =
                new GameService(new ExternalAPIGameHelperService(), new NMAlgorithmService());
        }

        // GET: Game
        public ActionResult Index()
        {
            return View();
        }
        
        // GET: Game/Play
        public async Task<ActionResult> Play(CancellationToken cancellationToken)
        {
            _logger.Debug("Received play request.");
            ResultViewModel resultViewModel = new ResultViewModel();

            try
            {
                CancellationToken linkedToken = CancellationTokenSource.CreateLinkedTokenSource(
                                    cancellationToken, Response.ClientDisconnectedToken).Token;

                GameContext gameContext = await _gameService.PlayAsync(linkedToken);

                if (gameContext.GameParameter == null || gameContext.GameResult == null)
                {
                    resultViewModel.Error = gameContext.Error;
                    resultViewModel.Winner = -1;
                }
                else
                {
                    resultViewModel.GameID = gameContext.GameParameter.GameID;
                    resultViewModel.ChildrenCount = gameContext.GameParameter.ChildrenCount;
                    resultViewModel.EliminateCount = gameContext.GameParameter.EliminateCount;
                    resultViewModel.Winner = gameContext.GameResult.LastChild;
                    resultViewModel.EliminateOrder = gameContext.GameResult.EliminationOrder;
                    resultViewModel.Error = gameContext.Error;
                }
            }
            catch (OperationCanceledException)
            {
                resultViewModel.Error = "Operation Cancelled.";
            }
            return View("Index", resultViewModel);
        }
    }
}