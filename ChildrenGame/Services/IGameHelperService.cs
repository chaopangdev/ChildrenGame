using ChildrenGame.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ChildrenGame.Services
{
    /// <summary>
    /// An interface describes the methods a GameHelperService exposes
    /// </summary>
    public interface IGameHelperService
    {
        /// <summary>
        /// Load the game parameters asynchronously 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>The game parameter object, null if failed</returns>
        Task<GameParameter> InitializeGameAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Handle the game result asynchronously
        /// </summary>
        /// <param name="gameResult"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The handling result, true if sucessful</returns>
        Task<bool> HandleGameResultAsync(GameResult gameResult, CancellationToken cancellationToken);
    }
}
