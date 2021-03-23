using game1024Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace game1024Core.Services
{
    public class RatingServiceFile : IRatingService
    {
        private List<Rating> _ratings = new List<Rating>();
        private const string FileName = "ratings.bin";

        public void AddRating(Rating rating)
        {
            _ratings.Add(rating);
            SaveRatings();
        }

        public IList<Rating> GetAllRatings()
        {
            LoadRatings();
            return _ratings;
        }

        public IList<Rating> GetLatestRatings()
        {
            LoadRatings();
            return _ratings.OrderByDescending(r => r.SubmittedAt).Take(3).ToList();
        }

        public double GetFinalRating()
        {
            LoadRatings();
            double count = _ratings.Count;
            double addition = 0;
            foreach (var rating in _ratings)
            {
                addition += rating.Value;
            }

            return Math.Round((addition / count), 1);
        }

        public void ResetRatings()
        {
            _ratings.Clear();
            File.Delete(FileName);
        }

        private void SaveRatings()
        {
            using (var fs = File.OpenWrite(FileName))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, _ratings);
            }
        }

        private void LoadRatings()
        {
            if (File.Exists(FileName))
            {
                using (var fs = File.OpenRead(FileName))
                {
                    var bf = new BinaryFormatter();
                    _ratings = (List<Rating>)bf.Deserialize(fs);
                }
            }
        }
    }
}
