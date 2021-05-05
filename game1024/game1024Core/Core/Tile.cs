using System;

namespace game1024Core.Core
{
    [Serializable]
    public class Tile
    {
        public Tile(int value)
        {
            Value = value;
            IsMerged = false;
        }

        public Tile(int value, bool isNew)
        {
            Value = value;
            IsNew = isNew;
            IsMerged = false;
        }

        public Tile(int value, bool isNew, bool isMerged)
        {
            Value = value;
            IsNew = isNew;
            IsMerged = isMerged;
        }

        public int Value { get; set; }
        public bool IsNew { get; set; }
        public bool IsMerged { get; set; }
    }
}