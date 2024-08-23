using _15Puzzle.Scripts.CanvasSystem;
using _15Puzzle.Scripts.Cell;
using _15Puzzle.Scripts.Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _15Puzzle.Scripts.UI
{
    public class PeekButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private CellManager _cellManager;
        public void OnPointerDown(PointerEventData eventData)
        {
            _cellManager = FindObjectOfType<CellManager>();
            foreach (var cell in _cellManager.cells)
            {
                cell.ChangeShowNumText(true);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            foreach (var cell in _cellManager.cells)
            {
                cell.ChangeShowNumText(!GameManager.Instance.gameIsStart);
            }
        }
    }
}