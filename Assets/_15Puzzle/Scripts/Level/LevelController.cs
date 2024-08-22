using _15Puzzle.Scripts.Cell;
using UnityEngine;

namespace _15Puzzle.Scripts.Level
{
    public class LevelController : MonoBehaviour
    {
        public CellManager cellManager;
        public Vector2Int gridLimits;
        public float cellDistance;
        public Vector3 levelPosition;

        public void Initialize()
        {
            LevelManager.Instance.gridLimits = gridLimits;
            cellManager.transform.localPosition = levelPosition;
            
            StartCoroutine(cellManager.ShuffleAnimationForCells(cellDistance));
        }
    }
}