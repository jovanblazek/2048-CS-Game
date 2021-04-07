using System;
using System.Collections.Generic;
using System.Text;

namespace game1024Core.Entities
{
    [Serializable]
    public class Rating
    {
        public int Id { get; set; }
        public string Player { get; set; }
        public int Value { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
