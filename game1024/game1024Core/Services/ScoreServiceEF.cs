using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game1024Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace game1024Core.Services
{
    public class ScoreServiceEF : IScoreService
    {
        public void AddScore(Score score)
        {
            using (var context = new Game1024DbContext())
            {
                context.Scores.Add(score);
                context.SaveChanges();
            }
        }

        public IList<Score> GetTopScores()
        {
            using (var context = new Game1024DbContext())
            {
                return (from s in context.Scores orderby s.Points descending select s).Take(3).ToList();
            }
        }

        public void ResetScore()
        {
            using (var context = new Game1024DbContext())
            {
                context.Database.ExecuteSqlRaw("DELETE FROM Scores");
            }
        }
    }
}
