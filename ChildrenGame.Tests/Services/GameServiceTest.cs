using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChildrenGame.Services;
using ChildrenGame.Models;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace ChildrenGame.Tests.Services
{
    [TestClass]
    public class GameServiceTest
    {
        [TestMethod]
        public void AllSuccessful()
        {
            GameParameter gameParameter = new GameParameter
            {
                GameID = "12345",
                ChildrenCount = 4,
                EliminateCount = 3
            };
            List<int> eliminateOrder = new List<int>(new[] { 3, 2, 4, 1 });


            Mock<IGameHelperService> mockHelperService = new Mock<IGameHelperService>();
            mockHelperService.Setup(p => p.InitializeGameAsync(
                It.IsAny<CancellationToken>())).Returns(Task.FromResult(gameParameter));
            mockHelperService.Setup(p => p.HandleGameResultAsync(
                It.IsAny<GameResult>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            Mock<IGameAlgorithmService> mockAlgorithmService = new Mock<IGameAlgorithmService>();
            mockAlgorithmService.Setup(p => 
                p.Process(It.IsAny<int>(), It.IsAny<int>())).Returns(eliminateOrder);
            

            GameService service = new GameService(mockHelperService.Object, mockAlgorithmService.Object);
            GameContext gameContext = service.PlayAsync(It.IsAny<CancellationToken>()).Result;

            Assert.AreEqual("12345", gameContext.GameParameter.GameID);

            Assert.AreEqual(1, gameContext.GameResult.LastChild);

            Assert.IsNull(gameContext.Error);
        }

        [TestMethod]
        public void GameParameterFailedToLoad()
        {
            Mock<IGameHelperService> mockHelperService = new Mock<IGameHelperService>();
            mockHelperService.Setup(p => p.InitializeGameAsync(It.IsAny<CancellationToken>())).Returns(
                Task.FromResult<GameParameter>(null));

            GameService service = new GameService(mockHelperService.Object, new NMAlgorithmService());
            GameContext gameContext = service.PlayAsync(It.IsAny<CancellationToken>()).Result;

            Assert.AreEqual("Failed to load parameters.", gameContext.Error);
        }

        [TestMethod]
        public void BadGameParameterZeroChild()
        {
            GameParameter badGameParameter = new GameParameter
            {
                ChildrenCount = 0,
                GameID = "12345",
                EliminateCount = 3
            };

            Mock<IGameHelperService> mockHelperService = new Mock<IGameHelperService>();
            mockHelperService.Setup(p => p.InitializeGameAsync(It.IsAny<CancellationToken>())).Returns(
                Task.FromResult(badGameParameter));

            GameService service = new GameService(mockHelperService.Object, new NMAlgorithmService());
            GameContext gameContext = service.PlayAsync(It.IsAny<CancellationToken>()).Result;

            Assert.AreEqual(
                "Wrong parameters loaded : children count (0), eliminate count (3)", gameContext.Error);
        }
    }
}
