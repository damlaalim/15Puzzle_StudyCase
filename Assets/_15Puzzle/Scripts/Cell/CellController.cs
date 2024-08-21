using System.Collections;
using UnityEngine;

namespace _15Puzzle.Scripts.Cell
{
    public class CellController : MonoBehaviour
    {
        public Vector2Int gridPosition;
        public bool isTouchable = true;

        [SerializeField] private float _swipeTime;
        [SerializeField] private AnimationCurve _curve;
        
        public void Swipe(Vector2 direction)
        {
            var pos = transform.position;
            var targetPos = new Vector3(pos.x + direction.x, pos.y + direction.y, pos.z);
            
            gridPosition = new Vector2Int(gridPosition.x + (int)direction.x, gridPosition.y + (int)direction.y);

            StartCoroutine(Swipe_Routine(targetPos));
        }

        private IEnumerator Swipe_Routine(Vector3 targetPos)
        {
            isTouchable = false;
            var elapsed = 0f;
            var initPos = transform.position;
            
            while (elapsed <= _swipeTime)
            {
                var normalized = elapsed / _swipeTime;
                transform.position = Vector3.Lerp(initPos, targetPos, _curve.Evaluate(normalized));
                
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPos;
            isTouchable = true;
        }
    }
}
