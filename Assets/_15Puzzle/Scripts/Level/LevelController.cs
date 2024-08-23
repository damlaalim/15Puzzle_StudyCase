using System.Collections.Generic;
using _15Puzzle.Scripts.CanvasSystem;
using _15Puzzle.Scripts.Cell;
using _15Puzzle.Scripts.Data;
using _15Puzzle.Scripts.Manager;
using UnityEngine;

namespace _15Puzzle.Scripts.Level
{
    public class LevelController : MonoBehaviour
    {
        public CellManager cellManager;
        public Vector2Int gridLimits;
        public float cellDistance;
        public Vector3 levelPosition;
        public GameType gameType = GameType.Classic;
        public GameMode gameMode = GameMode.Normal;

        private Dictionary<int, Vector2Int> _spiralPositions; 
        
        public void Initialize()
        {
            CanvasManager.Instance.Open(CanvasType.InGameCanvas);
            GameManager.Instance.NewLevelLoaded();
            LevelManager.Instance.gridLimits = gridLimits;
            TimeManager.Instance.EndLevel();
            
            cellManager.transform.localPosition = levelPosition;
            
            CacheSpiralPositions(gridLimits.x, gridLimits.y);
            
            cellManager.Initialize();
            StartCoroutine(cellManager.ShuffleAnimationForCells(cellDistance));

            InGameCanvas.Instance.ShowPeekButton(gameMode == GameMode.Hard);
            InGameCanvas.Instance.ChangeGameModeText(gameMode.ToString());
            InGameCanvas.Instance.ChangeGameTypeText(gameType.ToString());
            
        }
        
        private void CacheSpiralPositions(int width, int height)
        {
            _spiralPositions = new Dictionary<int, Vector2Int>();
            
            int x = 0, y = height - 1;
            var dirX = 1; // -1 => left, 1 => right
            var dirY = 0; // -1 => down, 1 => up
            
            // borders set the boundaries where cells can go. Borders are updated every time the direction changes 
            int borderUp = height - 2, borderLeft = 0, borderRight = width - 1, borderDown = 0;
            
            for (var i = 1; i < width * height; i++)
            {
                _spiralPositions.Add(i, new Vector2Int(x, y));

                x += dirX;
                y += dirY;
                
                // when limits are crossed, directions are changed
                if (x > borderRight && dirX == 1)
                {
                    dirX = 0; // The x-axis will no longer change
                    dirY = -1; // move down on the y-axis
                    x = borderRight; // x equals the limit again because it exceeds the limit
                    y--; // the y-axis of the next cell is updated by decrementing by one
                    borderRight--; // the limit is reduced by one so that you can no longer go to the right
                }
                else if (x < borderLeft && dirX == -1)
                {
                    dirX = 0;
                    dirY = 1;
                    x = borderLeft;
                    y++;
                    borderLeft++;
                }
                else if (y < borderDown && dirY == -1)
                {
                    dirY = 0;
                    dirX = -1;
                    y = borderDown;
                    x--;
                    borderDown++;
                }
                else if (y > borderUp && dirY == 1)
                {
                    dirY = 0;
                    dirX = 1;
                    y = borderUp;
                    x++;
                    borderUp--;
                }
            }
        }
        
        public Vector2Int GetSpiralPosition(int index)
        {
            if (_spiralPositions != null && _spiralPositions.TryGetValue(index, out var position))
            {
                return position;
            }

            return new Vector2Int(-1, -1);
        }
    }
}