using _15Puzzle.Scripts.Cell;
using _15Puzzle.Scripts.Manager;
using _15Puzzle.Scripts.Record;
using UnityEngine;

namespace _15Puzzle.Scripts.CanvasSystem
{
    public class SettingsCanvas : CanvasController
    {
        [SerializeField] private RecordsDisplay _recordsDisplay;
        private CellManager _cellManager;
        
        public override void Open()
        {
            base.Open();
            _recordsDisplay.DisplayRecord();
            GameManager.Instance.gameIsPause = true;
            _cellManager = FindObjectOfType<CellManager>();
            if (_cellManager)
                _cellManager.ShowCells(false);
        }

        public override void Close()
        {
            base.Close();
            GameManager.Instance.gameIsPause = false;
            if (_cellManager)
                _cellManager.ShowCells(true);
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