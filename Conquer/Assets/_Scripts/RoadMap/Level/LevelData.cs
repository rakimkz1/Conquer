using System;

namespace Level
{
    [Serializable]
    public struct LevelData 
    {
        public int levelOrder;
        public bool isBlocked;
        public int starNumber;
        public LevelData(int levelOrder, bool isBlocked, int starNumber)
        {
            this.levelOrder = levelOrder;
            this.isBlocked = isBlocked;
            this.starNumber = starNumber;
        }
    }
}