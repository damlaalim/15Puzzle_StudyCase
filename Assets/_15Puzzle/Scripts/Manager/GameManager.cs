using _15Puzzle.Scripts.Data;
using TMPro;
using UnityEngine;

namespace _15Puzzle.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; } 
        public bool gameIsStart = false, gameIsPause = false;

        [SerializeField] private TextMeshProUGUI _moveText;
        
        public int Move
        {
            get => _move;
            set
            {
                _move = value;
                _moveText.text = _move.ToString();
            }
        }

        private int _move;
        
        private void Awake()
        {
            Instance ??= this;
        }

        public void NewLevelLoaded()
        {
            gameIsStart = false;
            Move = 0;
        }
    }
}