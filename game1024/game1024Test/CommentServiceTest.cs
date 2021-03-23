using System;
using System.Collections.Generic;
using System.Text;
using game1024Core.Entities;
using game1024Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace game1024Test
{
    [TestClass]
    public class CommentServiceTest
    {
        [TestMethod]
        public void ACreateServiceTest()
        {
            var service = CreateService();
            Assert.AreEqual(0, service.GetLatestComments().Count);
        }

        [TestMethod]
        public void AddCommentTest()
        {
            var service = CreateService();
            service.AddComment(new Comment{ Player = "Jovan", Text = "Toto je komentar", SubmittedAt = DateTime.Now});

            Assert.AreEqual(1, service.GetLatestComments().Count);
            Assert.AreEqual("Jovan", service.GetLatestComments()[0].Player);
            Assert.AreEqual("Toto je komentar", service.GetLatestComments()[0].Text);
        }

        [TestMethod]
        public void AddMultipleCommentsTest()
        {
            var service = CreateService();
            service.AddComment(new Comment { Player = "Jovan", Text = "Toto je komentar1", SubmittedAt = DateTime.Now });
            service.AddComment(new Comment { Player = "Jaro", Text = "Toto je komentar2", SubmittedAt = DateTime.Now });
            service.AddComment(new Comment { Player = "Jozo", Text = "Toto je komentar3", SubmittedAt = DateTime.Now });

            Assert.AreEqual(3, service.GetLatestComments().Count);

            Assert.AreEqual("Jovan", service.GetLatestComments()[2].Player);
            Assert.AreEqual("Toto je komentar1", service.GetLatestComments()[2].Text);

            Assert.AreEqual("Jaro", service.GetLatestComments()[1].Player);
            Assert.AreEqual("Toto je komentar2", service.GetLatestComments()[1].Text);

            Assert.AreEqual("Jozo", service.GetLatestComments()[0].Player);
            Assert.AreEqual("Toto je komentar3", service.GetLatestComments()[0].Text);
        }

        [TestMethod]
        public void ResetCommentsTest()
        {
            var service = CreateService();
            service.AddComment(new Comment { Player = "Jovan", Text = "Toto je komentar", SubmittedAt = DateTime.Now });

            service.ResetComments();
            Assert.AreEqual(0, service.GetLatestComments().Count);
        }

        private ICommentService CreateService()
        {
            return new CommentServiceFile();
        }
    }
}
