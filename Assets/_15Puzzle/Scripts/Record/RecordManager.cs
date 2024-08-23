using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _15Puzzle.Scripts.Record
{
    public class RecordManager : MonoBehaviour
    {
        public static RecordManager Instance { get; private set; } 
        private string filePath;
        private LevelRecords levelRecords;

        private void Awake()
        {
            Instance ??= this;
        }
        
        private void Start()
        {
            // level data is saved in a json file
            filePath = Application.persistentDataPath + "/levelRecords.json";
            LoadRecords();
        }

        public void SaveRecord(int levelNumber, float timeTaken, int moveCount)
        {
            LevelRecord newRecord = new LevelRecord()
            {
                levelNumber = levelNumber,
                timeTaken = timeTaken,
                dateCompleted = System.DateTime.Now.ToString("dd-MM-yyyy"),
                moveCount = moveCount
            };

            levelRecords.records.Add(newRecord);
            string json = JsonUtility.ToJson(levelRecords, true);
            File.WriteAllText(filePath, json);
        }

        public void LoadRecords()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                levelRecords = JsonUtility.FromJson<LevelRecords>(json);
            }
            else
            {
                levelRecords = new LevelRecords();
            }
        }

        public List<LevelRecord> GetRecords()
        {
            return levelRecords.records;
        }
    }
}