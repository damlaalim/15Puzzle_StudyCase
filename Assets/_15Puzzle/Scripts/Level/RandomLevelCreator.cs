using System.Collections.Generic;
using _15Puzzle.Scripts.Cell;
using UnityEngine;

namespace _15Puzzle.Scripts.Level
{
    public class RandomLevelCreator : MonoBehaviour
    {
        [SerializeField] private int _rows, _cols;
        [SerializeField] private GameObject _cellPrefab, _cellManagerPrefab;

        public void GenerateGrid()
        {
            var cellManagerObj = Instantiate(_cellManagerPrefab);
            var cellManager = cellManagerObj.GetComponent<CellManager>();

            for (var i = 0; i < _rows; i++)
            {
                for (var j = 0; j < _cols; j++)
                {
                    var newCell = Instantiate(_cellPrefab, cellManagerObj.transform);
                    newCell.transform.localPosition = new Vector3(i, j, 0);
                    newCell.name = $"Cell - {i}, {j}";
                    var cellController = newCell.GetComponent<CellController>();
                    cellController.gridPosition = new Vector2Int(i, j);
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