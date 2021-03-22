using System;
using System.Collections.Generic;
using System.Text;
using game1024Core.Entities;

namespace game1024Core.Services
{
    public interface IScoreService
    {
        void AddScore(Score score);

        IList<Score> GetTopScores();

        void ResetScore();
    }
}
