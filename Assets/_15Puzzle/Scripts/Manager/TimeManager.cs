using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace _15Puzzle.Scripts.Manager
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }
        public double GetElapsedTime => _elapsedTime;
        
        [SerializeField] private TextMeshProUGUI _timeText;
        
        private double _elapsedTime = 0;
        private Coroutine _timeRoutine;
        
        private void Awake()
        {
            Instance ??= this;
        }
        
        public void StartTime()
        {
            if (_timeRoutine is not null)
                StopCoroutine(_timeRoutine);
            
            _timeRoutine = StartCoroutine(Time_Routine());
        }

        private IEnumerator Time_Routine()
        {
            while (GameManager.Instance.gameIsStart)
            {
                _elapsedTime++;
                var time = TimeSpan.FromSeconds(_elapsedTime);
                _timeText.text = time.ToString(@"mm\:ss");
                yield return new WaitForSeconds(1);
            }
        }

        public void StopTime()
        {
            if (_timeRoutine is not null)
                StopCoroutine(_timeRoutine);
        }

        public void EndLevel()
        {
            _elapsedTime = 0;
            _timeText.text = "00:00";
        }
    }
}