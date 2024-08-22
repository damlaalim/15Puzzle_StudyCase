using System.Collections.Generic;
using _15Puzzle.Scripts.Cell;
using UnityEngine;
using UnityEngine.UI;

namespace _15Puzzle.Scripts.Level
{
    public class RandomLevelCreator : MonoBehaviour
    {
        [SerializeField] private int _rows, _cols;
        [SerializeField] private float _cellDistance;
        [SerializeField] private GameObject _cellPrefab, _cellManagerPrefab, _levelControllerPrefab;
        [SerializeField] private Vector3 _cellManagerPos;
        [SerializeField] private float scaleFactor = 0.8f; 
        [SerializeField] private float baseScale = 1f;
        
        public void GenerateGrid()
        {
            var levelControllerObj = Instantiate(_levelControllerPrefab);
            
            var cellManagerObj = Instantiate(_cellManagerPrefab, levelControllerObj.transform);
            cellManagerObj.transform.localPosition = _cellManagerPos;
            var cellManager = cellManagerObj.GetComponent<CellManager>();
            
            int maxGridDimension = Mathf.Max(_rows, _cols);
            float scaleAdjustment = Mathf.Pow(scaleFactor, maxGridDimension - 3); 
            float newScale = baseScale * scaleAdjustment;
            cellManagerObj.transform.localScale = new Vector3(newScale, newScale, 1f);

            var levelController = levelControllerObj.GetComponent<LevelController>();
            levelController.cellManager = cellManager;
            levelController.gridLimits = new Vector2Int(_rows, _cols);
            levelController.cellDistance = _cellDistance;

            for (var i = 0; i < _rows; i++)
            {
                for (var j = 0; j < _cols; j++)
                {
                    var newCell = Instantiate(_cellPrefab, cellManagerObj.transform);
                    newCell.transform.localPosition = new Vector3(i * _cellDistance, j * _cellDistance, 0);
                    newCell.name = $"Cell - {i}, {j}";
                    var cellController = newCell.GetComponent<CellController>();
                    cellController.gridPosition = new Vector2Int(i, j);
                    cellController.cellManager = cellManager;
                    cellController.distance = _cellDistance;
                    
                    cellManager.cells.Add(cellController);
                }
            }

            RemoveRandomCell(cellManager);
            NumberTheCells(cellManager);
        }

        private void RemoveRandomCell(CellManager cellManager)
        {
            var randomIndex = Random.Range(0, cellManager.cells.Count);
            DestroyImmediate(cellManager.cells[randomIndex].gameObject);
            cellManager.cells.RemoveAt(randomIndex);
        }

        private void NumberTheCells(CellManager cellManager)
        {
            var numList = new List<int>();

            for (int i = 0; i < cellManager.cells.Count; i++)
            {
                numList.Add(i + 1);
            }

            foreach (var cell in cellManager.cells)
            {
                var randomIndex = Random.Range(0, numList.Count);
                cell.number = numList[randomIndex];
                cell.ChangeNumberText(numList[randomIndex].ToString());
                numList.RemoveAt(randomIndex);
            }
        }
    }
}