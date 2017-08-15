using Newtonsoft.Json;
using System.Text;

namespace ChildrenGame.Models
{
    /// <summary>
    /// The game parameter object used in services
    /// </summary>
    public class GameParameter
    {
        /// <summary>
        /// Game ID
        /// </summary>
        [JsonProperty("id")]
        public string GameID { get; set; }

        /// <summary>
        /// Children Count
        /// </summary>
        [JsonProperty("children_count")]
        public int ChildrenCount { get; set; }

        /// <summary>
        /// Eliminate Count
        /// </summary>
        [JsonProperty("eliminate_each")]
        public int EliminateCount { get; set; }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("GameID: " + (GameID ?? ""));
            strBuilder.Append(",ChildrenCount: " + ChildrenCount);
            strBuilder.Append(",EliminateCount: " + EliminateCount);

            return strBuilder.ToString();
        }
    }
}