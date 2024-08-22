using _15Puzzle.Scripts.Data;
using UnityEngine;

namespace _15Puzzle.Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; } 
        public GameType gameType = GameType.Classic;
        
        private void Awake()
        {
            Instance ??= this;
        }
    }
}