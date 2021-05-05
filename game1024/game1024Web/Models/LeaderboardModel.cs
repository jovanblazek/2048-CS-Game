using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game1024Core.Entities;

namespace game1024Web.Models
{
    public class LeaderboardModel
    {
        public IList<Score> Scores { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<Rating> Ratings { get; set; }
        public double FinalRating { get; set; }
    }
}
