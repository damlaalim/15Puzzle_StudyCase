using System.Collections.Generic;
using UnityEngine;

namespace _15Puzzle.Scripts.Level
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; } 
        
        public int GetLevel => LevelNumber;
        public Vector2Int gridLimits;
        public List<LevelController> levels;

        private int LevelNumber
        {
            get => PlayerPrefs.GetInt("level", 0);
            set => PlayerPrefs.SetInt("level", LevelNumber + 1 >= levels.Count ? 0 : value);
        }

        private LevelController _currentLevel;

        private void Awake()
        {
            Instance ??= this;
        }

        private void Start()
        {
            Load();
        }
        
        private void Save()
        {
            LevelNumber += 1;
        }

        public void Load()
        {
            if (_currentLevel is not null)
                Destroy(_currentLevel.gameObject);

            _currentLevel = Instantiate(levels[LevelNumber]);
            _currentLevel.Initialize();
        }

        public void NextLevel()
        {
            Save();
            Load();
        }
    }
}