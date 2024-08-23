using _15Puzzle.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace _15Puzzle.Scripts.CanvasSystem
{
    public class InGameCanvas : CanvasController
    {
        public static InGameCanvas Instance { get; private set; }

        [SerializeField] private GameObject _peekBtn;

        public void ShowPeekButton(bool show) => _peekBtn.SetActive(show);

        private void Awake()
        {
            Instance ??= this;
        }
        
        public void OpenSettingsCanvas()
        {
            CanvasManager.Instance.Open(CanvasType.Settings);
        }
    }
}