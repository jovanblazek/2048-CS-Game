using System;
using game1024Core.Entities;
using game1024Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace game1024Test
{
    [TestClass]
    public class ScoreServiceTest
    {
        [TestMethod]
        public void CreateServiceTest()
        {
            var service = CreateService();
            Assert.AreEqual(0, service.GetTopScores().Count);
        }

        [TestMethod]
        public void AddScoreTestSingle()
        {
            var service = CreateService();
            service.AddScore(new Score{Player = "Jovan", Points = 100, PlayedAt = DateTime.Now});

            Assert.AreEqual(1, service.GetTopScores().Count);
            Assert.AreEqual("Jovan", service.GetTopScores()[0].Player);
            Assert.AreEqual(100, service.GetTopScores()[0].Points);
        }
        [TestMethod]
        public void AddScoreTestMultiple()
        {
            var service = CreateService();
            service.AddScore(new Score { Player = "Jovan", Points = 100, PlayedAt = DateTime.Now });
            service.AddScore(new Score { Player = "Jano", Points = 200, PlayedAt = DateTime.Now });
            service.AddScore(new Score { Player = "Jozo", Points = 50, PlayedAt = DateTime.Now });

            Assert.AreEqual(3, service.GetTopScores().Count);

            Assert.AreEqual("Jano", service.GetTopScores()[0].Player);
            Assert.AreEqual(200, service.GetTopScores()[0].Points);

            Assert.AreEqual("Jovan", service.GetTopScores()[1].Player);
            Assert.AreEqual(100, service.GetTopScores()[1].Points);

            Assert.AreEqual("Jozo", service.GetTopScores()[2].Player);
            Assert.AreEqual(50, service.GetTopScores()[2].Points);
        }

        [TestMethod]
        public void ResetScoreTest()
        {
            var service = CreateService();
            service.AddScore(new Score { Player = "Jovan", Points = 100, PlayedAt = DateTime.Now });

            service.ResetScore();
            Assert.AreEqual(0, service.GetTopScores().Count);
        }


        private IScoreService CreateService()
        {
            return new ScoreServiceFile();
        }
    }
}
