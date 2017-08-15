using ChildrenGame.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ChildrenGame.Services
{
    /// <summary>
    /// An interface discribes the methods exposed to controller layer
    /// </summary>
    interface IGameService
    {
        /// <summary>
        /// Play the game asynchronously, including loading parameters, processing calculation and handling result.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>The game context object, which contains game parameters and game result</returns>
        Task<GameContext> PlayAsync(CancellationToken cancellationToken);
    }
}
