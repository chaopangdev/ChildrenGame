using System.Collections.Generic;

namespace ChildrenGame.Services
{
    /// <summary>
    /// An O(nm) complexity implementation, in which 'n' is the children count and 'm' is the elimination count
    /// </summary>
    public class NMAlgorithmService : IGameAlgorithmService
    {
        /// <summary>
        /// Process the game to get elimination children order list.
        /// This list also includes the last child who wins the game.
        /// In the elimination order list, child index starts from 1 instead of 0. 
        /// </summary>
        /// <param name="totalCount">total children count</param>
        /// <param name="skip">the elimination count</param>
        /// <returns>The list of elimination order</returns>
        public List<int> Process(int totalCount, int skip)
        {
            int position = -1;                          //children index
            int count = 0;                              //count the children until the skip one
            int[] children;                     
            List<int> eliminatedList = new List<int>();

            if (skip <= 0 || totalCount <= 0)
                return eliminatedList;

            children = new int[totalCount];             //use array as the children circle

            while (eliminatedList.Count < totalCount)
            {
                while (count != skip)
                {
                    position = (position + 1) % totalCount; //make the array acting like a circle
                    if (children[position] == 0)            //if the child is still in the circle
                        count++;
                }

                children[position] = 1;                 //set the child as eliminated from the circle
                eliminatedList.Add(position + 1);       //add the child index to the elimination list, child index starts from 1
                count = 0;
            }

            return eliminatedList;
        }
    }
}