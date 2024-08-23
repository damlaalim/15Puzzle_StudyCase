using _15Puzzle.Scripts.Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _15Puzzle.Scripts.Cell
{
    public class CellTouchController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        private CellController _cellController;
        private void Start()
        {
            _cellController = GetComponent<CellController>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_cellController.cellManager.cellsIsTouchable && !GameManager.Instance.gameIsPause && !_cellController.cellManager.cellShuffle)
                _cellController.cellManager.SwipeCells(_cellController);
        }
    }
}