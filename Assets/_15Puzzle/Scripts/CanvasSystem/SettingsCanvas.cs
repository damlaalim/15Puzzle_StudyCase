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
    }
}