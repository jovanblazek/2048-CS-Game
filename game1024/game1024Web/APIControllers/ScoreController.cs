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
    [Route("api/Score")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly IScoreService _scoreService = new ScoreServiceEF();

        //GET: /api/Score
        [HttpGet]
        public IEnumerable<Score> GetScores()
        {
            return _scoreService.GetTopScores();
        }

        //POST: /api/Score
        [HttpPost]
        public void PostScore(Score score)
        {
            score.PlayedAt = DateTime.Now;
            _scoreService.AddScore(score);
        }
    }
}
