using System.Collections.Generic;
using _15Puzzle.Scripts.Level;
using UnityEngine;

namespace _15Puzzle.Scripts.Cell
{
    public class CellManager : MonoBehaviour
    {
        public List<CellController> cells;
        [SerializeField] private LevelManager _levelManager;

        public void SwipeCells(CellController targetCell)
        {
            var targetX = targetCell.gridPosition.x;
            var targetY = targetCell.gridPosition.y;

            // the axes of the selected cell are checked for gap respectively
            if (CellSwipeControl(targetX, targetY, targetX, true, true, new Vector2(-1,0)))
                return;
            if (CellSwipeControl(targetX, targetY, targetY, true, false, new Vector2(0, -1)))
                return;
            if (CellSwipeControl(targetX, targetY, targetX, false, true, new Vector2(1, 0)))
                return;
            if (CellSwipeControl(targetX, targetY, targetY, false, false, new Vector2(0, 1)))
                return;
        }

        private bool CellSwipeControl(int targetX, int targetY, int targetAxis, bool isNegative, bool isXAxis, Vector2 dir)
        {
            // Swipe to the selected cell from the opposite direction of the cell to be swiped

            var findTheSpace = false;

            // I added different conditions to fit all directions
            
            var i = isNegative ? 0 : isXAxis ? _levelManager.gridLimits.x -1 : _levelManager.gridLimits.y -1; 
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
    }
}