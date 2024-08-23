using System.Collections;
using System.Collections.Generic;
using _15Puzzle.Scripts.Data;
using _15Puzzle.Scripts.Level;
using _15Puzzle.Scripts.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _15Puzzle.Scripts.Cell
{
    public class CellManager : MonoBehaviour
    {
        public List<CellController> cells;
        public bool cellsIsTouchable;
        public LevelController levelController;
        
        [SerializeField] private int _shuffleAnimationCount = 4;
        [SerializeField] private float _shuffleTime = .2f;

        public void Initialize()
        {
            cellsIsTouchable = false;
            foreach (var cell in cells)
            {
                cell.Initialize();
            }    
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
            var gridLimits = LevelManager.Instance.gridLimits;
            // I added different conditions to fit all directions

            var i = isNegative ? 0 : isXAxis ? gridLimits.x - 1 : gridLimits.y - 1;
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

            if (findTheSpace)
                GameManager.Instance.Move++;
            
            return findTheSpace;
        }

        public void ControlCellCorrectPositions()
        {
            foreach (var cell in cells)
            {
                if (levelController.gameType == GameType.Classic && !cell.CalculateCorrectPositionForClassic())
                    return;
                if (levelController.gameType == GameType.Snake && !cell.CalculateCorrectPositionForSnake())
                    return;
                if (levelController.gameType == GameType.Spiral && !cell.CalculateCorrectPositionForSpiral())
                    return;
            }

            LevelManager.Instance.NextLevel();
        }

        public IEnumerator ShuffleAnimationForCells(float distance)
        {
            yield return new WaitForSeconds(.7f);
            var gridLimits = LevelManager.Instance.gridLimits;

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
                        var randomX = Random.Range(0, gridLimits.x);
                        var randomY = Random.Range(0, gridLimits.y);
                        var randomPos = new Vector3(randomX * distance, randomY * distance, 0);

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
                yield return new WaitForSeconds(.2f);
                foreach (var cell in cells)
                {
                    StartCoroutine(cell.Shake_Routine(.3f, 10));
                }

                yield return new WaitForSeconds(.3f);
            }

            // They return to their original position in the level
            foreach (var cell in cells)
            {
                var originalPos = new Vector3(cell.gridPosition.x * distance, cell.gridPosition.y * distance, 0);
                StartCoroutine(cell.Swipe_Routine(originalPos));
            }

            yield return new WaitForSeconds(.3f);
            cellsIsTouchable = true;
        }

        public void ChangeNumberShowCells()
        {
            foreach (var cell in cells)
            {
                cell.ChangeShowNumText(levelController.gameMode == GameMode.Normal);
            }
        }

        public void ShowCells(bool show)
        {
            foreach (var cell in cells)
            {
                cell.gameObject.SetActive(show);
            }
        }
    }
}