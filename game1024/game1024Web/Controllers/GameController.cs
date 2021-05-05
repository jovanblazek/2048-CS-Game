using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using game1024Core.Core;
using game1024Core.Entities;
using game1024Core.Services;
using game1024Web.Models;
using Microsoft.Extensions.Logging;

namespace game1024Web.Controllers
{
    public class GameController : Controller
    {
        private const string GameSessionKey = "hra";
        private IScoreService _scoreService = new ScoreServiceEF();
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var game = (Game)HttpContext.Session.GetObject(GameSessionKey);
            if (game == null)
                game = new Game(4);
            HttpContext.Session.SetObject(GameSessionKey, game);
            return View("Index", game);
        }

        public IActionResult Move(Direction dir)
        {
            var game = (Game) HttpContext.Session.GetObject(GameSessionKey);
            game.Update(dir);
            HttpContext.Session.SetObject(GameSessionKey, game);
            return View("Index", game);
        }

        public IActionResult New()
        {
            var game = new Game(4);
            HttpContext.Session.SetObject(GameSessionKey, game);
            return View("Index", game);
        }
        public IActionResult SaveScore(Score score)
        {
            score.PlayedAt = DateTime.Now;
            _scoreService.AddScore(score);
            return New();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
