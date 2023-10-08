using System;
using System.Collections.Generic;

namespace GasHero.Scoreboard
{
    //Esta clase se encarga de guardar los datos de la estructura "ScoreboardEntryData" en una lista de datos compuestos(nombre y puntuación)
    //Luego seran convertidas a un archivo JSON a fin de guardarlas en el dispositivo
    [Serializable]
    public class ScoreboardSaveData
    {
        public List<ScoreboardEntryData> highscores = new List<ScoreboardEntryData>();       
    }   

}


