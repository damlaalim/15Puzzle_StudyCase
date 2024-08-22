using _15Puzzle.Scripts.Cell;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace _15Puzzle.Scripts.InputControl
{
    public class InputController : MonoBehaviour
    {
        public static InputController Instance { get; private set; } 

        public CellManager cellManager;
        private Camera _cam;
        private Vector3 _offset, _startPos, _endPos;
        private bool _isTouching;
        private CellController _lastTouchedCell;

        private void Awake()
        {
            Instance ??= this;
        }
        
        private void Start()
        {
            _cam = Camera.main;
        }

        // private void Update()
        // {
        //     InputControl();
        // }

        // private void InputControl()
        // {
        //     if (Touchscreen.current.primaryTouch.press.isPressed)
        //     {
        //         var pointerData = new PointerEventData(eventSystem)
        //         {
        //             position = Touchscreen.current.primaryTouch.position.ReadValue()
        //         };
        //         
        //         var hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        //
        //         if (hit.collider != null && hit.transform.TryGetComponent<CellController>(out var cell) && cell.isTouchable)
        //         {
        //             _isTouching = true;
        //             _lastTouchedCell = cell;
        //         }
        //     }
        //     else if (_isTouching)
        //     {
        //         cellManager.SwipeCells(_lastTouchedCell);
        //         _isTouching = false;
        //     }
        // }
    }
}