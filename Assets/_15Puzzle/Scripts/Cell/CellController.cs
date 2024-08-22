using System.Collections;
using _15Puzzle.Scripts.Data;
using _15Puzzle.Scripts.Game;
using _15Puzzle.Scripts.Level;
using TMPro;
using UnityEngine;

namespace _15Puzzle.Scripts.Cell
{
    public class CellController : MonoBehaviour
    {
        public int number;
        public Vector2Int gridPosition;
        public CellManager cellManager;
        public float distance;
        public LevelController levelController;

        [SerializeField] private float _swipeTime, _scaleAnimDelay = .3f;
        [SerializeField] private Vector3 _targetScale = Vector3.one * 1.2f;
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private TextMeshProUGUI _numText;

        private bool correctPosFind;

        public void ChangeNumberText(string text) => _numText.text = text;
        
        public void Swipe(Vector2 direction)
        {
            var pos = transform.localPosition;
            
            var targetPos = new Vector3(pos.x + (direction.x * distance), pos.y + (direction.y  * distance), pos.z);

            gridPosition = new Vector2Int(gridPosition.x + (int)direction.x, gridPosition.y + (int)direction.y);

            StartCoroutine(Swipe_Routine(targetPos));

            // calculating the correct targeted position according to the type of game
            var newPosIsCorrect = GameManager.Instance.gameType switch
            {
                GameType.Classic => CalculateCorrectPositionForClassic(),
                GameType.Snake => CalculateCorrectPositionForSnake(),
                GameType.Spiral => CalculateCorrectPositionForSpiral(),
                _ => false
            };

            // when it is in the correct position, the position of the other cells is checked
            if (newPosIsCorrect)
                cellManager.ControlCellCorrectPositions();

            correctPosFind = newPosIsCorrect;
        }
        
        public IEnumerator Swipe_Routine(Vector3 targetPos)
        {
            cellManager.cellsIsTouchable = false;
            var elapsed = 0f;
            var initPos = transform.localPosition;
            
            while (elapsed <= _swipeTime)
            {
                var normalized = elapsed / _swipeTime;
                transform.localPosition = Vector3.Lerp(initPos, targetPos, _curve.Evaluate(normalized));
                
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = targetPos;
            cellManager.cellsIsTouchable = true;

            if (correctPosFind)
                StartCoroutine(ScaleAnimation_Routine());
        }

        private IEnumerator ScaleAnimation_Routine()
        {
            var init = 0f;
            while (init <= _scaleAnimDelay / 2)
            {
                var normalized = init / (_scaleAnimDelay / 2);
                transform.localScale = Vector3.Lerp(Vector3.one, _targetScale, normalized);
                
                init += Time.deltaTime;
                yield return 0;
            }

            transform.localScale = _targetScale;
            
            init = 0f;
            while (init <= _scaleAnimDelay / 2)
            {
                var normalized = init / (_scaleAnimDelay / 2);
                transform.localScale = Vector3.Lerp(_targetScale, Vector3.one, normalized);
                
                init += Time.deltaTime;
                yield return 0;
            }

            transform.localScale = Vector3.one;
        }

        public bool CalculateCorrectPositionForClassic()
        {
            var gridLimits = LevelManager.Instance.gridLimits;
            
            // calculate which position it should be in according to its number from left to right
            var column = (number - 1) % gridLimits.x;
            var row = (number - 1) / gridLimits.x;
            var y = gridLimits.y - 1 - row;
            
            return new Vector2Int(column, y) == gridPosition;
        }

        public bool CalculateCorrectPositionForSnake()
        {
            var index = number - 1;
            var gridLimits = LevelManager.Instance.gridLimits;
            var y = index / gridLimits.x;
            int x; 

            // If the line number is odd, we go from right to left, otherwise from left to right.
            if (y % 2 == 0)
                x = index % gridLimits.x; 
            else
                x = gridLimits.x - 1 - (index % gridLimits.x); 

            var correctPos = new Vector2Int(x, gridLimits.y - 1 - y);
            return correctPos == gridPosition; 
        }

        public bool CalculateCorrectPositionForSpiral()
        {
            var correctPos = levelController.GetSpiralPosition(number);
            return correctPos == gridPosition;
        }
    }
}