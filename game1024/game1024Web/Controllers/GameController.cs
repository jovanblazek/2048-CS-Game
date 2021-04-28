using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using game1024Core.Core;

namespace game1024Web.Controllers
{
    public class GameController : Controller
    {
        private const string GameSessionKey = "hra";

        public IActionResult Index()
        {
            var game = new Game(4);
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
    }
}
