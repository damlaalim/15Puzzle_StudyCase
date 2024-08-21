using System;
using System.Collections;
using _15Puzzle.Scripts.Level;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _15Puzzle.Scripts.Cell
{
    public class CellController : MonoBehaviour
    {
        public int number;
        public Vector2Int gridPosition;
        public bool isTouchable = true;

        [SerializeField] private float _swipeTime;
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private TextMeshProUGUI _numText;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private CellManager _cellManager;

        public void ChangeNumberText(string text) => _numText.text = text;
        
        public void Swipe(Vector2 direction)
        {
            var pos = transform.position;
            var targetPos = new Vector3(pos.x + direction.x, pos.y + direction.y, pos.z);
            
            gridPosition = new Vector2Int(gridPosition.x + (int)direction.x, gridPosition.y + (int)direction.y);

            StartCoroutine(Swipe_Routine(targetPos));
            var correctPos = CalculateCorrectPosition();

            // when it is in the correct position, the position of the other cells is checked
            if (correctPos)
                _cellManager.ControlCellCorrectPositions();
        }
        
        public bool CalculateCorrectPosition()
        {
            // calculate which position it should be in according to its number from left to right
            var column = (number - 1) % _levelManager.gridLimits.x;
            var row = (number - 1) / _levelManager.gridLimits.x;
            var y = _levelManager.gridLimits.y - 1 - row;
            
            return new Vector2Int(column, y) == gridPosition;
        }
        
        public IEnumerator Swipe_Routine(Vector3 targetPos)
        {
            isTouchable = false;
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
            isTouchable = true;
        }
    }
}
