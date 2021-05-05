using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game1024Core.Services;
using game1024Web.Models;

namespace game1024Web.Controllers
{
    public class LeaderboardController : Controller
    {
        private IScoreService _scoreService = new ScoreServiceEF();
        private ICommentService _commentService = new CommentServiceEF();
        private IRatingService _ratingService = new RatingServiceEF();


        public IActionResult Index()
        {
            return View("Index", CreateModel());
        }

        private LeaderboardModel CreateModel()
        {
            var scores = _scoreService.GetTopScores();
            var comments = _commentService.GetLatestComments();
            var ratings = _ratingService.GetLatestRatings();
            var rating = _ratingService.GetFinalRating();

            return new LeaderboardModel
            {
                Scores = scores, Comments = comments, Ratings = ratings, FinalRating = rating
            };
        }

    }
}
