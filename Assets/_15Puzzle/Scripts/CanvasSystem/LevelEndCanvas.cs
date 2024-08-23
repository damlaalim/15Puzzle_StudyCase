using System;
using _15Puzzle.Scripts.Level;
using _15Puzzle.Scripts.Manager;
using TMPro;
using UnityEngine;

namespace _15Puzzle.Scripts.CanvasSystem
{
    public class LevelEndCanvas : CanvasController
    {
        [SerializeField] private TextMeshProUGUI _timeText;

        public override void Open()
        {
            base.Open();
            
            var time = TimeSpan.FromSeconds(TimeManager.Instance.GetElapsedTime);
            _timeText.text = time.ToString(@"mm\:ss");
        }

        public void NextLevelButton()
        {
            LevelManager.Instance.Load();
        }
    }
}