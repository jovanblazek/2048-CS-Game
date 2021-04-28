using System;

namespace game1024Core.Core
{
    [Serializable]
    public class Tile
    {
        public Tile(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }
}