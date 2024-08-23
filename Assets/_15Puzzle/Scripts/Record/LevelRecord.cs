using System;

namespace _15Puzzle.Scripts.Record
{
    [Serializable]
    public class LevelRecord
    {
        public int levelNumber;
        public float timeTaken;
        public string dateCompleted;
        public int moveCount; 
    }
}