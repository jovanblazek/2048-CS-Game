using System;
using System.Collections.Generic;
using System.Text;
using game1024Core.Entities;
using game1024Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace game1024Test
{
    [TestClass]
    public class RatingServiceTest
    {
        [TestMethod]
        public void ACreateServiceTest()
        {
            var service = CreateService();
            Assert.AreEqual(0, service.GetAllRatings().Count);
        }

        [TestMethod]
        public void AddRatingTest()
        {
            var service = CreateService();
            service.ResetRatings();
            service.AddRating(new Rating { Player = "Jovan", Value = 7, SubmittedAt = DateTime.Now });

            Assert.AreEqual(1, service.GetLatestRatings().Count);
            Assert.AreEqual(1, service.GetAllRatings().Count);

            Assert.AreEqual("Jovan", service.GetAllRatings()[0].Player);
            Assert.AreEqual(7, service.GetAllRatings()[0].Value);
        }

        [TestMethod]
        public void AddMultipleRatingsTest()
        {
            var service = CreateService();
            service.ResetRatings();
            service.AddRating(new Rating { Player = "Jovan", Value = 7, SubmittedAt = DateTime.Now });
            service.AddRating(new Rating { Player = "Jaro", Value = 8, SubmittedAt = DateTime.Now });
            service.AddRating(new Rating { Player = "Jozo", Value = 9, SubmittedAt = DateTime.Now });

            Assert.AreEqual(3, service.GetLatestRatings().Count);

            Assert.AreEqual("Jovan", service.GetLatestRatings()[2].Player);
            Assert.AreEqual(7, service.GetLatestRatings()[2].Value);

            Assert.AreEqual("Jaro", service.GetLatestRatings()[1].Player);
            Assert.AreEqual(8, service.GetLatestRatings()[1].Value);

            Assert.AreEqual("Jozo", service.GetLatestRatings()[0].Player);
            Assert.AreEqual(9, service.GetLatestRatings()[0].Value);
        }

        [TestMethod]
        public void GetFinalRatingTest()
        {
            var service = CreateService();
            service.ResetRatings();
            service.AddRating(new Rating { Player = "Jovan", Value = 4, SubmittedAt = DateTime.Now });
            service.AddRating(new Rating { Player = "Jaro", Value = 5, SubmittedAt = DateTime.Now });

            Assert.AreEqual(4.5, service.GetFinalRating());

            service.AddRating(new Rating { Player = "Jozo", Value = 10, SubmittedAt = DateTime.Now });
            Assert.AreEqual(6.3, service.GetFinalRating());
        }

        [TestMethod]
        public void ResetRatingsTest()
        {
            var service = CreateService();
            service.AddRating(new Rating { Player = "Jovan", Value = 7, SubmittedAt = DateTime.Now });

            service.ResetRatings();
            Assert.AreEqual(0, service.GetAllRatings().Count);
        }

        private IRatingService CreateService()
        {
            return new RatingServiceEF();
        }
    }
}
