using _15Puzzle.Scripts.Data;
using UnityEngine;

namespace _15Puzzle.Scripts.CanvasSystem
{
    public class CanvasController : MonoBehaviour
    {
        public CanvasType canvasType;
        protected Canvas Canvas;
        
        public virtual void Initialize()
        {
            Canvas = GetComponent<Canvas>();
            Close();
        }

        public virtual void Close() => Canvas.enabled = false;

        public virtual void Open() => Canvas.enabled = true;

        public virtual void Back() => CanvasManager.Instance.Back();
    }
}