using System.Collections.Generic;
using _15Puzzle.Scripts.Level;
using UnityEngine;

namespace _15Puzzle.Scripts.Cell
{
    public class CellManager : MonoBehaviour
    {
        [SerializeField] private List<CellController> _cells;
        [SerializeField] private LevelManager _levelManager;

        public void SwipeCells(CellController targetCell, Vector2 dir)
        {
            var targetX = targetCell.gridPosition.x;
            var targetY = targetCell.gridPosition.y;
            
            // find the cells to be swiped according to the direction of the operation
            if (dir.x < 0)
                CellSwipeControl(targetX, targetY, targetX, true, true, dir);
            else if (dir.y < 0)
                CellSwipeControl(targetX, targetY, targetY, true, false, dir);
            else if (dir.x > 0)
                CellSwipeControl(targetX, targetY, targetX, false, true, dir);
            else if (dir.y > 0)
                CellSwipeControl(targetX, targetY, targetY, false, false, dir);
        }

        private void CellSwipeControl(int targetX, int targetY, int targetAxis, bool isNegative, bool isXAxis, Vector2 dir)
        {
            // Swipe to the selected cell from the opposite direction of the cell to be swiped

            var findTheSpace = false;

            // I added different conditions to fit all directions
            
            var i = isNegative ? 0 : isXAxis ? _levelManager.gridLimits.x -1 : _levelManager.gridLimits.y -1; 
            for (; isNegative ? i <= targetAxis : i >= targetAxis; i += isNegative ? 1 : -1)
            {
                // check whether the cell exists or not
                var cell = _cells.Find(cell => cell.gridPosition.x == (isXAxis ? i : targetX) 
                                               && cell.gridPosition.y == (isXAxis ? targetY : i));
                    
                // swiping starts after the gap is found
                if (findTheSpace && cell is not null)
                    cell.Swipe(dir);
                else if (cell is null)
                    findTheSpace = true;
            }
        }
    }
}