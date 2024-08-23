using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _15Puzzle.Scripts.Record
{
    public class RecordsDisplay : MonoBehaviour
    {
        public Transform recordsParent;
        public GameObject recordPrefab;

        public void DisplayRecord()
        {
            DisplayRecords();
        }

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
            }
        }
    }
}