using System;
using System.Collections;
using System.Collections.Generic;
using _15Puzzle.Scripts.Level;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _15Puzzle.Scripts.Cell
{
    public class CellManager : MonoBehaviour
    {
        public List<CellController> cells;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private int _shuffleAnimationCount;
        [SerializeField] private float _shuffleTime;

        private void Start()
        {
            StartCoroutine(ShuffleAnimationForCells());
        }
        
        public void SwipeCells(CellController targetCell)
        {
            var targetX = targetCell.gridPosition.x;
            var targetY = targetCell.gridPosition.y;

            // the axes of the selected cell are checked for gap respectively
            if (CellSwipeControl(targetX, targetY, targetX, true, true, new Vector2(-1, 0)))
                return;
            if (CellSwipeControl(targetX, targetY, targetY, true, false, new Vector2(0, -1)))
                return;
            if (CellSwipeControl(targetX, targetY, targetX, false, true, new Vector2(1, 0)))
                return;
            if (CellSwipeControl(targetX, targetY, targetY, false, false, new Vector2(0, 1)))
                return;
        }

        private bool CellSwipeControl(int targetX, int targetY, int targetAxis, bool isNegative, bool isXAxis,
            Vector2 dir)
        {
            // Swipe to the selected cell from the opposite direction of the cell to be swiped

            var findTheSpace = false;

            // I added different conditions to fit all directions

            var i = isNegative ? 0 : isXAxis ? _levelManager.gridLimits.x - 1 : _levelManager.gridLimits.y - 1;
            for (; isNegative ? i <= targetAxis : i >= targetAxis; i += isNegative ? 1 : -1)
            {
                // check whether the cell exists or not
                var cell = cells.Find(cell => cell.gridPosition.x == (isXAxis ? i : targetX)
                                              && cell.gridPosition.y == (isXAxis ? targetY : i));

                // swiping starts after the gap is found
                if (findTheSpace && cell is not null)
                    cell.Swipe(dir);
                else if (cell is null)
                    findTheSpace = true;
            }

            return findTheSpace;
        }

        public void ControlCellCorrectPositions()
        {
            foreach (var cell in cells)
            {
                if (!cell.CalculateCorrectPosition())
                    return;
            }

            Debug.Log("game over");
        }

        private IEnumerator ShuffleAnimationForCells()
        {
            // run the shuffle animation as many times as you want it to run
            for (var i = 0; i < _shuffleAnimationCount; i++)
            {
                var createdPositions = new List<Vector2>();
                
                // each cell is assigned a random position
                foreach (var cell in cells)
                {
                    // a previously uncreated position is created
                    while (true)
                    {
                        var randomX = Random.Range(0, _levelManager.gridLimits.x);
                        var randomY = Random.Range(0, _levelManager.gridLimits.y);
                        var randomPos = new Vector3(randomX, randomY, 0);

                        // returns to the beginning of the loop if a position has already been created
                        if (createdPositions.Contains(randomPos))
                        {
                            yield return 0;
                            continue;
                        }

                        StartCoroutine(cell.Swipe_Routine(randomPos));
                        createdPositions.Add(randomPos);

                        break;
                    }
                }

                yield return new WaitForSeconds(_shuffleTime);
            }

            // They return to their original position in the level
            foreach (var cell in cells)
            {
                var originalPos = new Vector3(cell.gridPosition.x, cell.gridPosition.y, 0);
                StartCoroutine(cell.Swipe_Routine(originalPos));
            }
        }
    }
}