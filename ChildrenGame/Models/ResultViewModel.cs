
namespace ChildrenGame.Models
{
    /// <summary>
    /// The result view model used between controller and view layers
    /// </summary>
    public class ResultViewModel
    {
        /// <summary>
        /// Game ID
        /// </summary>
        public string GameID { get; set; }
        
        /// <summary>
        /// Children Count
        /// </summary>
        public int ChildrenCount { get; set; }

        /// <summary>
        /// Eliminate Count
        /// </summary>
        public int EliminateCount { get; set; }

        /// <summary>
        /// Winner
        /// </summary>
        public int Winner { get; set; }

        /// <summary>
        /// Eliminate Order
        /// </summary>
        public int[] EliminateOrder { get; set; }

        /// <summary>
        /// Error String
        /// </summary>
        public string Error { get; set; }
        
    }
}