using Newtonsoft.Json;

namespace ChildrenGame.Models
{
    /// <summary>
    /// The game result object used in services
    /// </summary>
    public class GameResult
    {
        /// <summary>
        /// Game ID
        /// </summary>
        [JsonProperty("id")]
        public string GameID { get; set; }

        /// <summary>
        /// Winner
        /// </summary>
        [JsonProperty("last_child")]
        public int LastChild { get; set; }

        /// <summary>
        /// Elimination Order, the winner is not included
        /// </summary>
        [JsonProperty("order_of_elimination")]
        public int[] EliminationOrder { get; set; }
    }
}