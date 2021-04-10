using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game1024Core.Entities;
using game1024Core.Services;

namespace game1024Web.APIControllers
{
    [Route("api/Rating")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService = new RatingServiceEF();

        //GET: /api/Rating
        [HttpGet]
        public IEnumerable<Rating> GetRatings()
        {
            return _ratingService.GetLatestRatings();
        }

        //POST: /api/Rating
        [HttpPost]
        public void PostRating(Rating rating)
        {
            rating.SubmittedAt = DateTime.Now;
            _ratingService.AddRating(rating);
        }

        //GET: /api/Rating/all
        [HttpGet]
        [Route("all")]
        public IEnumerable<Rating> GetAllRatings()
        {
            return _ratingService.GetAllRatings();
        }

        //GET: /api/Rating/final
        [HttpGet]
        [Route("final")]
        public double GetFinalRating()
        {
            return _ratingService.GetFinalRating();
        }
    }
}
