﻿using System;

namespace game1024Core.Entities
{
    [Serializable]
    public class Score
    {
        public int Id { get; set; }
        public string Player { get; set; }
        public int Points { get; set; }
        public DateTime PlayedAt { get; set; }
    }
}