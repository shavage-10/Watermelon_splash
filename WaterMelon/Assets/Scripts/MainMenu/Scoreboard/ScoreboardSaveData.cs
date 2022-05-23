using System;
using System.Collections.Generic;


namespace Jesper.Scoreboards
{
    [Serializable]
    public class ScoreboardSaveData 
    {
        public List<ScoreboardEntryData> Highscores = new List<ScoreboardEntryData>();
    }
}

