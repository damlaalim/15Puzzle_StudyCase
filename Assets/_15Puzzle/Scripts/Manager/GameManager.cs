using _15Puzzle.Scripts.Data;
using UnityEngine;

namespace _15Puzzle.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; } 
        public GameType gameType = GameType.Classic;
        public bool gameIsStart = false;
        
        private void Awake()
        {
            Instance ??= this;
        }
    }
}