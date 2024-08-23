using _15Puzzle.Scripts.Manager;
using _15Puzzle.Scripts.Record;
using UnityEngine;

namespace _15Puzzle.Scripts.CanvasSystem
{
    public class SettingsCanvas : CanvasController
    {
        [SerializeField] private RecordsDisplay _recordsDisplay;
        
        public override void Open()
        {
            base.Open();
            _recordsDisplay.DisplayRecord();
        }

        public void MusicOn()
        {
            AudioManager.Instance.SetMusicValue(1);
        }
        
        public void MusicOff()
        {
            AudioManager.Instance.SetMusicValue(0);
        }
        
        public void SoundOn()
        {
            AudioManager.Instance.SetEffectValue(1);
        }
        
        public void SoundOff()
        {
            AudioManager.Instance.SetEffectValue(0);
        }
        
    }
}