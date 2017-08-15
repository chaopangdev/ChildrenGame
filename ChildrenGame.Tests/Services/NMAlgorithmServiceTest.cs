using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChildrenGame.Services;

namespace ChildrenGame.Tests.Services
{
    [TestClass]
    public class GameHandlerTest
    {
        NMAlgorithmService _NMAlgorithmService = new NMAlgorithmService();

        [TestMethod]
        public void ChildrenCountAndEliminateCountBothPositive()
        {
            int[] eliminateOrder = _NMAlgorithmService.Process(4, 2).ToArray();
            CollectionAssert.AreEqual(new[] { 2, 4, 3, 1 }, eliminateOrder);
        }

        [TestMethod]
        public void OnlyOneChild()
        {
            int[] eliminateOrder = _NMAlgorithmService.Process(1, 2).ToArray();
            CollectionAssert.AreEqual(new[] { 1 }, eliminateOrder);
        }

        [TestMethod]
        public void ChildrenCountIsZero()
        {
            int[] eliminateOrder = _NMAlgorithmService.Process(0, 2).ToArray();
            Assert.AreEqual(0, eliminateOrder.Length);
        }

        [TestMethod]
        public void ChildrenCountIsNegative()
        {
            int[] eliminateOrder = _NMAlgorithmService.Process(-1, 2).ToArray();
            Assert.AreEqual(0, eliminateOrder.Length);
        }

        [TestMethod]
        public void EliminaeCountIsZero()
        {
            int[] eliminateOrder = _NMAlgorithmService.Process(5, 0).ToArray();
            Assert.AreEqual(0, eliminateOrder.Length);
        }

        [TestMethod]
        public void EliminaeCountIsNegative()
        {
            int[] eliminateOrder = _NMAlgorithmService.Process(5, -1).ToArray();
            Assert.AreEqual(0, eliminateOrder.Length);
        }

    }
}
