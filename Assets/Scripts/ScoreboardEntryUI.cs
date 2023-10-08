using TMPro;
using UnityEngine;

namespace GasHero.Scoreboard
{
        public class ScoreboardEntryUI : MonoBehaviour //Esta clase se encarga de almacenar datos ingresados en variables de tipo TextMesh
    {
        //Variables de tipo TextMeshProUGUI inicializadas como nulas
        [SerializeField] private TextMeshProUGUI entryNameText = null;
        [SerializeField] private TextMeshProUGUI entryScoreText = null;       
        

        // Método que recibe datos de clase ScoreboardEntryData y las almacena en sus variables TextMesh segun corresponda a name o score
        public void Initialise(ScoreboardEntryData scoreboardEntryData)
        {
            entryNameText.text = scoreboardEntryData.entryName;
            entryScoreText.text = scoreboardEntryData.entryScore.ToString();            
        }
    }
}


