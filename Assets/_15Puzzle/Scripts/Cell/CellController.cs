using System.Collections;
using _15Puzzle.Scripts.Data;
using _15Puzzle.Scripts.Level;
using _15Puzzle.Scripts.Manager;
using TMPro;
using UnityEngine;
using AudioType = _15Puzzle.Scripts.Data.AudioType;

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

        public void Initialize()
        { 
            StartCoroutine(Animation_Routine());
        }

        private IEnumerator Animation_Routine()
        {
            yield return ScaleAnimation_Routine(Vector3.zero, Vector3.one, .2f);
            yield return new WaitForSeconds(.1f);
            yield return Shake_Routine(.1f, 10);
        }
        
        public void ChangeNumberText(string text) => _numText.text = text;

        public void ChangeShowNumText(bool show) => _numText.enabled = show;

        public void Swipe(Vector2 direction)
        {
            // starts the timer when the first move is made
            if (!GameManager.Instance.gameIsStart)
            {
                GameManager.Instance.gameIsStart = true;
                TimeManager.Instance.StartTime();
                cellManager.ChangeNumberShowCells();
            }

            AudioManager.Instance.PlayEffect(AudioType.Tap);
            var pos = transform.localPosition;
            var targetPos = new Vector3(pos.x + (direction.x * distance), pos.y + (direction.y * distance), pos.z);
            gridPosition = new Vector2Int(gridPosition.x + (int)direction.x, gridPosition.y + (int)direction.y);

            StartCoroutine(Swipe_Routine(targetPos));

            // calculating the correct targeted position according to the type of game
            var newPosIsCorrect = levelController.gameType switch
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

        #region AnimationRoutines

        
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
                StartCoroutine(HoverAnimation_Routine());
        }

        private IEnumerator HoverAnimation_Routine()
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

        private IEnumerator ScaleAnimation_Routine(Vector3 startScale, Vector3 endScale, float time)
        {
            var elapsed = 0f;
            transform.localScale = startScale;

            while (elapsed <= time)
            {
                var normalized = elapsed / time;
                transform.localScale = Vector3.Lerp(startScale, endScale, normalized);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localScale = endScale;
        }

        public IEnumerator Shake_Routine(float duration, float magnitude)
        {
            Vector3 originalPosition = transform.localPosition;

            float elapsed = 0.0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition =
                    new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

                elapsed += Time.deltaTime;

                yield return null; 
            }

            transform.localPosition = originalPosition;
        }

        #endregion

        #region CorrectPositionCalculaters

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

        #endregion
    }
}