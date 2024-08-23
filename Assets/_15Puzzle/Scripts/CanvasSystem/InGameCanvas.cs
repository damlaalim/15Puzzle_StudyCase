using _15Puzzle.Scripts.Cell;
using _15Puzzle.Scripts.Data;
using _15Puzzle.Scripts.Level;
using _15Puzzle.Scripts.Manager;
using TMPro;
using UnityEngine;

namespace _15Puzzle.Scripts.CanvasSystem
{
    public class InGameCanvas : CanvasController
    {
        public static InGameCanvas Instance { get; private set; }

        [SerializeField] private GameObject _peekBtn;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _gameTypeText;
        [SerializeField] private TextMeshProUGUI _gameModeText;
        [SerializeField] private GameObject _pauseUI;

        private CellManager _cellManager;

        public void ShowPeekButton(bool show) => _peekBtn.SetActive(show);
        public void ChangeLevelText(string level) => _levelText.text = "lvl " + level;
        public void ChangeGameTypeText(string type) => _gameTypeText.text = type;
        public void ChangeGameModeText(string mode) => _gameModeText.text = mode;

        private void Awake()
        {
            Instance ??= this;
        }

        public override void Open()
        {
            base.Open();
            ChangeLevelText((LevelManager.Instance.GetLevel + 1).ToString());
        }

        public void OpenSettingsCanvas()
        {
            CanvasManager.Instance.Open(CanvasType.Settings);
        }

        public void PauseGame()
        {
            _pauseUI.SetActive(true);
            TimeManager.Instance.StopTime();
            _cellManager = FindObjectOfType<CellManager>();
            _cellManager.ShowCells(false);
        }

        public void ResumeGame()
        {
            _pauseUI.SetActive(false);
            _cellManager.ShowCells(true);
            TimeManager.Instance.StartTime();
        }

        public void RefreshGame()
        {
            TimeManager.Instance.EndLevel();
            LevelManager.Instance.Load();
            GameManager.Instance.gameIsStart = false;
        }
    }
}