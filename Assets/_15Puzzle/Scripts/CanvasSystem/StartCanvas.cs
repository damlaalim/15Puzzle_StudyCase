using _15Puzzle.Scripts.Data;
using _15Puzzle.Scripts.Level;

namespace _15Puzzle.Scripts.CanvasSystem
{
    public class StartCanvas : CanvasController
    {
        public void StartGame()
        {
            CanvasManager.Instance.Open(CanvasType.InGameCanvas);
            LevelManager.Instance.Load();
        }

        public void OpenSettings()
        {
            CanvasManager.Instance.Open(CanvasType.Settings);
        }
    }
}