using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game1024Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace game1024Core.Services
{
    public class RatingServiceEF : IRatingService
    {
        public void AddRating(Rating rating)
        {
            using (var context = new Game1024DbContext())
            {
                context.Ratings.Add(rating);
                context.SaveChanges();
            }
        }

        public IList<Rating> GetLatestRatings()
        {
            using (var context = new Game1024DbContext())
            {
                return (from r in context.Ratings orderby r.SubmittedAt descending select r).Take(3).ToList();
            }
        }

        public IList<Rating> GetAllRatings()
        {
            using (var context = new Game1024DbContext())
            {
                return (from r in context.Ratings select r).ToList();
            }
        }

        public double GetFinalRating()
        {
            using (var context = new Game1024DbContext())
            {
                IList<Rating> ratings = (from r in context.Ratings select r).ToList();
                double count = ratings.Count;
                double addition = 0;
                foreach (var rating in ratings)
                {
                    addition += rating.Value;
                }

                return Math.Round((addition / count), 1);
            }
        }

        public void ResetRatings()
        {
            using (var context = new Game1024DbContext())
            {
                context.Database.ExecuteSqlRaw("DELETE FROM Ratings");
            }
        }
    }
}
