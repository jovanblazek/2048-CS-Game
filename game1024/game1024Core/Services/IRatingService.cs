using System;
using System.Collections.Generic;
using System.Text;
using game1024Core.Entities;

namespace game1024Core.Services
{
    public interface IRatingService
    {
        void AddRating(Rating rating);

        IList<Rating> GetLatestRatings();

        IList<Rating> GetAllRatings();

        double GetFinalRating();

        void ResetRatings();
    }
}
