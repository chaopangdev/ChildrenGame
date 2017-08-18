using System.Collections.Generic;

namespace ChildrenGame.Services
{
    /// <summary>
    /// An interface describes methods a GameAlgorithmService exposes
    /// </summary>
    public interface IGameAlgorithmService
    {
        /// <summary>
        /// Process the calculation to get the eliminated children order list
        /// </summary>
        /// <param name="totalCount">total children count</param>
        /// <param name="skip">the elimination count</param>
        /// <returns>The list of children eliminated order, including the last child</returns>
        List<int> Process(int totalCount, int skip);
    }
}
