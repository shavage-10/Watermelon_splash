using System.IO;
using UnityEngine;

namespace Jesper.Scoreboards
{
    public class Scoreboard : MonoBehaviour
    {
        [SerializeField] private int MaxScoreboardEntries = 5;
        [SerializeField] private Transform highscoresHolderTransform = null;
        [SerializeField] private GameObject scoreboardEntryObject = null;
        [Header("Test")]
        [SerializeField] private ScoreboardEntryData testEntryData = new ScoreboardEntryData();

        private string SavePath => $"{Application.persistentDataPath}/highscores.json";
        private void Start()
        {
            ScoreboardSaveData savedScores = GetSavedScores();
            UpdateUI(savedScores);
            SaveScore(savedScores);

        }
        [ContextMenu("Add test entry")] 
        public void AddTestEntry()
        {
            Addentry(testEntryData);
        }
        public void Addentry(ScoreboardEntryData scoreboardEntryData)
        {
            ScoreboardSaveData savedScores = GetSavedScores();
            bool scoreAdded = false;

            for (int i = 0; i < savedScores.Highscores.Count; i++)
            {
                if(scoreboardEntryData.entryScore > savedScores.Highscores[i].entryScore)
                {
                    savedScores.Highscores.Insert(i, scoreboardEntryData);
                    scoreAdded = true;
                    break;
                }
            }

             if(!scoreAdded && savedScores.Highscores.Count < MaxScoreboardEntries)
            {
                savedScores.Highscores.Add(scoreboardEntryData);
            }
             if(savedScores.Highscores.Count > MaxScoreboardEntries)
            {
                savedScores.Highscores.RemoveRange(MaxScoreboardEntries, savedScores.Highscores.Count - MaxScoreboardEntries);
            }

            UpdateUI(savedScores);
            SaveScore(savedScores);
        }

         
        private void UpdateUI(ScoreboardSaveData savedScores)
        {
            foreach(Transform child in highscoresHolderTransform)
            {
                Destroy(child.gameObject);
            }
            foreach(ScoreboardEntryData highscore in savedScores.Highscores)
            {
                Instantiate(scoreboardEntryObject, highscoresHolderTransform).
                    GetComponent<ScoreboardEntryUI>().Initialise(highscore);
            }
        }
        private ScoreboardSaveData GetSavedScores()
        {
            if (!File.Exists(SavePath))
            {
                File.Create(SavePath).Dispose();
                return new ScoreboardSaveData();
            }
            using(StreamReader stream = new StreamReader(SavePath))
            {
                string json = stream.ReadToEnd();
                return JsonUtility.FromJson<ScoreboardSaveData>(json);
            }
        }
        private void SaveScore(ScoreboardSaveData scoreboardSaveData)
        {
            using (StreamWriter stream = new StreamWriter(SavePath))
            {
                string json = JsonUtility.ToJson(scoreboardSaveData, true);
                stream.Write(json); 

            }
        }
    }
}


