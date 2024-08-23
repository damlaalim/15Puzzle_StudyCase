using _15Puzzle.Scripts.Data;

namespace _15Puzzle.Scripts.CanvasSystem
{
    public class InGameCanvas : CanvasController
    {
        public void OpenSettingsCanvas()
        {
            CanvasManager.Instance.Open(CanvasType.Settings);
        }
    }
}