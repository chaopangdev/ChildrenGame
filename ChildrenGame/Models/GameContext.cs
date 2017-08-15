
namespace ChildrenGame.Models
{
    /// <summary>
    /// The game context object used between service and controller layers
    /// </summary>
    public class GameContext
    {
        /// <summary>
        /// Game Parameter
        /// </summary>
        public GameParameter GameParameter { get; set; }

        /// <summary>
        /// Game Result
        /// </summary>
        public GameResult GameResult { get; set; }

        /// <summary>
        /// Error String
        /// </summary>
        public string Error { get; set; }
    }
}