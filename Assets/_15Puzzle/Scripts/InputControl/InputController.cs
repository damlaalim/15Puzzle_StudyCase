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
        private Touch _touch;
        private bool _isDragging;
        private CellController _lastTouchedCell;

        #region EventFunctions

        private void Awake()
        {
            _touch = new Touch();
        }

        private void Start()
        {
            _cam = Camera.main;
        }

        private void OnEnable()
        {
            _touch.Enable();
        }

        private void OnDisable()
        {
            _touch.Disable();
        }

        private void Update()
        {
            InputControl();
        }

        #endregion

        #region Inputs

        private Vector2 GetTouchThisFrame()
        {
            return _touch.TouchInputs.Direction.ReadValue<Vector2>();
        }

        #endregion

        private void InputControl()
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                var touchPosition = GetTouchThisFrame();
                var worldPosition =
                    _cam.ScreenToWorldPoint(Input.mousePosition);

                var hit = Physics2D.Raycast(worldPosition, Vector2.zero);

                // as long as the cell is swiped, if will run and the swipe will be executed when the cell is not found
                if (hit.collider != null && hit.transform.TryGetComponent<CellController>(out var cell) && cell.isTouchable)
                {
                    _isDragging = true;
                    _lastTouchedCell = cell;
                    _startPos = touchPosition;
                }
                else if (_isDragging)
                {
                    SwipeCells();
                }
            }
            else if (_isDragging)
                _isDragging = false;
        }

        private void SwipeCells()
        {
            _endPos = GetTouchThisFrame();
            var dragDir = _endPos - _startPos;

            // I checked which direction to swipe according to the input entered
            
            if (dragDir.x < 0)
                _cellManager.SwipeCells(_lastTouchedCell, new Vector2(-1, 0));
            else if (dragDir.x > 0)
                _cellManager.SwipeCells(_lastTouchedCell, new Vector2(1, 0));
            else if (dragDir.y < 0)
                _cellManager.SwipeCells(_lastTouchedCell, new Vector2(0, -1));
            else if (dragDir.y > 0)
                _cellManager.SwipeCells(_lastTouchedCell, new Vector2(0, 1));

            _isDragging = false;
        }
    }
}