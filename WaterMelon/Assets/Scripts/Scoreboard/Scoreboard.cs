using System.IO;
using UnityEngine;

namespace Jesper.Scoreboards
{
    public class Scoreboard : MonoBehaviour
    {
        [SerializeField] private int MaxScoreboardEntries = 5;
        [SerializeField] private Transform highscoresHolderTransform = null;
        [SerializeField] private GameObject scoreboardEntryObject = null;

        private string SavePath => $"{Application.persistentDataPath}/highscores.json";
        private void Start()
        {
            ScoreboardSaveData savedScores = GetSavedScores();
            UpdateUI(savedScores);
            SaveScore(savedScores);

        }
        public void Addentry(ScoreboardEntryData scoreboardEntryData) // The Method to add entries to the scoreboard logic
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

         
        private void UpdateUI(ScoreboardSaveData savedScores) //Taking the scoreboard and updating the UI to match the data collected from the logic
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
        private ScoreboardSaveData GetSavedScores() // A method which looks if a savefile is precent, reads the whole file and saves it into the json string and convert it into the correct datatype.</summary>
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
        private void SaveScore(ScoreboardSaveData scoreboardSaveData) // a method which writes the whole file with the help of the SavePath function
        {
            using (StreamWriter stream = new StreamWriter(SavePath))
            {
                string json = JsonUtility.ToJson(scoreboardSaveData, true);
                stream.Write(json); 

            }
        }
    }
}


