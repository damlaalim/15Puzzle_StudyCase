using _15Puzzle.Scripts.Cell;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _15Puzzle.Scripts.InputControl
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private CellManager _cellManager;
        
        private Camera _cam;
        private Vector3 _offset, _startPos, _endPos;
        private bool _isTouching;
        private CellController _lastTouchedCell;

        private void Start()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            InputControl();
        }

        private void InputControl()
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                var worldPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(worldPosition, Vector2.zero);

                if (hit.collider != null && hit.transform.TryGetComponent<CellController>(out var cell) && cell.isTouchable)
                {
                    _isTouching = true;
                    _lastTouchedCell = cell;
                }
            }
            else if (_isTouching)
            {
                _cellManager.SwipeCells(_lastTouchedCell);
                _isTouching = false;
            }
        }
    }
}