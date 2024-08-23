using System;
using _15Puzzle.Scripts.Level;
using _15Puzzle.Scripts.Manager;
using TMPro;
using UnityEngine;
using AudioType = _15Puzzle.Scripts.Data.AudioType;

namespace _15Puzzle.Scripts.CanvasSystem
{
    public class LevelEndCanvas : CanvasController
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private ParticleSystem _confetti;

        public override void Open()
        {
            base.Open();
            
            var time = TimeSpan.FromSeconds(TimeManager.Instance.GetElapsedTime);
            _timeText.text = time.ToString(@"mm\:ss");
            _confetti.Play();
            AudioManager.Instance.PlayEffect(AudioType.Success);
        }

        public void NextLevelButton()
        {
            LevelManager.Instance.Load();
        }
    }
}