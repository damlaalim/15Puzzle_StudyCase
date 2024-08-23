using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _15Puzzle.Scripts.Record
{
    public class RecordsDisplay : MonoBehaviour
    {
        public Transform recordsParent;
        public GameObject recordPrefab;

        private List<GameObject> createdRecords = new List<GameObject>();

        public void DisplayRecord()
        {
            for (int i = 0; i < createdRecords.Count; i++)
            {
                DestroyImmediate(createdRecords[i]);
            }
            createdRecords.Clear();
            DisplayRecords();
        }

        // new objects are created and record settings are saved
        private void DisplayRecords()
        {
            List<LevelRecord> records = RecordManager.Instance.GetRecords();
            
            foreach (var record in records)
            {
                var time = TimeSpan.FromSeconds(record.timeTaken);
                var newTime = time.ToString(@"mm\:ss");
             
                GameObject recordObj = Instantiate(recordPrefab, recordsParent);
                recordObj.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = (record.levelNumber+1).ToString();
                recordObj.transform.Find("TimeText").GetComponent<TextMeshProUGUI>().text = newTime.ToString();
                recordObj.transform.Find("DateText").GetComponent<TextMeshProUGUI>().text = record.dateCompleted;
                recordObj.transform.Find("MovesText").GetComponent<TextMeshProUGUI>().text = record.moveCount.ToString();
                
                createdRecords.Add(recordObj);
            }
        }
    }
}