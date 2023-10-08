using System.IO; //IO se usa para escribir y leer archivos independientemente de la plataforma utilizada
using UnityEngine;

namespace GasHero.Scoreboard
{    
    public class Scoreboard : MonoBehaviour //Esta clase se encarga de ingresar, guardar y actualizar puntuaciones en la UI.
    {
        [SerializeField] private int maxScoreboardEntries = 5; // Maxima cantidad de puntajes que se alamacenaran en la tabla de puntuaciones

        [SerializeField] private Transform highscoresHolderTransform = null; //Referencia al Transform del gameObject "Holder_HighScore" que contiene el prefab "Entry_Highscore"
                
        [SerializeField] private GameObject scoreboardEntryObject = null; //Referencia al prefab que da visibilidad al nombre y score "Entry_Highscore"


        [Header("Test")]
        [SerializeField] ScoreboardEntryData testEntrydata = new ScoreboardEntryData(); //Declara variable de tipo ScoreboardEntryData y le asigna un objeto nuevo para el test


        //variables para cargar datos de entrada
        private int score;
        private string nombre; 

        private string SavePath => $"{Application.persistentDataPath}/highscores.json"; //SavePath devuelve la ruta de acceso al archivo "highscore.json"

        private void Start()
        {            
            ScoreboardSaveData savedScores = GetSavedScores(); //Carga en la variable "savedScore" las puntuaciones guardadas en archivo .json
                        
            UpdateUI(savedScores); //Actualiza la informacion de la interfaz segun la info obtenida en la linea anterior
                        
            SaveScores(savedScores);    //Guarda lista actualizada en el archivo .json   

            if (PlayerPrefs.HasKey("Puntaje")) //agregue el && PlayerPrefs.HasKey("TextoIngresado")
            {                
                score = PlayerPrefs.GetInt("Puntaje"); //Obtiene los datos persistentes
                nombre = PlayerPrefs.GetString("TextoIngresado");               

                testEntrydata.entryName = nombre;//Almacena nombre ingresado por player en arreglo
                testEntrydata.entryScore = score;//Almacena score logrado por player en arreglo                 

                AddEntry(testEntrydata);//Ingresa los datos nuevos al sistema de Highscore

                //Elimina los datos persistentes 
                PlayerPrefs.DeleteKey("Puntaje");
                PlayerPrefs.DeleteKey("TextoIngresado");
               
            }          
            
        }
        
        //[ContextMenu("AddTestEntry")]   
        // public void AddTestEntry()
        //{
        //    AddEntry(testEntrydata);
        //}
        public void AddEntry(ScoreboardEntryData scoreboardEntryData) //Funcion que agrega una entrada en la tabla de puntuaciones, actualiza el UI y guarda 
        {

            ScoreboardSaveData savedScores = GetSavedScores();//Obtenemos puntuaciones guardadas

            bool scoreAdded = false;  //verifica si se agrego, o no, el puntaje                                                  
            
            for (int i = 0; i < savedScores.highscores.Count; i++) //Recorremos la lista "highscores" para cargar la puntuación nueva en una POSICION correcta
            {               

                if (scoreboardEntryData.entryScore > savedScores.highscores[i].entryScore)//Si el puntaje ingresado es mayor que alguno de los puntajes de la lista
                {
                    savedScores.highscores.Insert(i, scoreboardEntryData);               //Inserta el nuevo puntaje en la posicion iterada actualmente                    
                    scoreAdded = true;                                     
                    break;                                                                //sale del ciclo para no agregar puntaje en ninguna otra posición posterior
                }
                
            }
            
            if (!scoreAdded && savedScores.highscores.Count < maxScoreboardEntries) //Si no se encuentra una posición adecuada y no se supero el maximo de entradas
            {                
                savedScores.highscores.Add(scoreboardEntryData);                    //se agrega la nueva entrada al final de la lista.
            }
                       
            if (savedScores.highscores.Count > maxScoreboardEntries) //Si supera el maximo de entradas admitidas, se eliminan las entradas adicionales
            {
                savedScores.highscores.RemoveRange(maxScoreboardEntries, savedScores.highscores.Count - maxScoreboardEntries);
            }

            UpdateUI(savedScores);//Se utiliza la funcion Update UI para actualizar la informacion en la interfaz
            SaveScores(savedScores); //Guarda los datos actualizados en el archivo .json
        }

       
        private void UpdateUI(ScoreboardSaveData savedScores)  //Actualiza la interfaz del usuario segun puntuacion guardada
        {
            
            foreach (Transform child in highscoresHolderTransform) //Bucle que itera sobre los hijos del objeto "highscoreHolderTransform" y los elimina para cargar nueva informacion
            {
                Destroy(child.gameObject);       
            }

            foreach (ScoreboardEntryData highscore in savedScores.highscores) //Bucle que itera sobre cada entrada de puntuación guardada en la coleccion "highscores"
            {
                Instantiate(scoreboardEntryObject, highscoresHolderTransform).  //Para cada entrada, instancia un objeto "ScoreboardEntryUI" que es colocado como hijo del objeto "highscoresHolderTransform"
                GetComponent<ScoreboardEntryUI>().Initialise(highscore);    //"Initialise" convierte en TextMesh los valores de entrada actual.           
            }
        }

        
        private ScoreboardSaveData GetSavedScores() //Funcion que de un archivo JSON devuelve un objeto "ScoreboardSaveData"
        {
           
            if (!File.Exists(SavePath)) //Comprueba si el archivo "highscores.json" existe en la ubicacion "SavePath"
            {                
                File.Create(SavePath).Dispose();//si no existe, crea un archivo nuevo y usa "Dispose()" para liberar los recursos utilizados para la creacion y guardado de archivo
                return new ScoreboardSaveData();//Devuelve un nuevo objeto "ScoreboardSaveData" vacio
            }
                           
            using (StreamReader stream = new StreamReader(SavePath)) //Si el archivo ya existiera, lee todo el contenido del archivo ubicado en "SavePath" mediante un objeto StreamReader        
            {                
                string json = stream.ReadToEnd(); //Almacena la informacion leida en una variable llamada "json"                               
                return JsonUtility.FromJson<ScoreboardSaveData>(json); //Con la función "FromJson" convierte el contenido de "json" a un objeto "ScoreboardSaveData"
            }

        }
        
        private void SaveScores(ScoreboardSaveData scoreboardSaveData) //Función para guardar puntajes
        {            
            using (StreamWriter stream = new StreamWriter(SavePath)) //Se instancia un objeto de tipo StreamWriter para escribir en un archivo hallado en la ruta "SavePath"
            {                
                string json = JsonUtility.ToJson(scoreboardSaveData, true); //Los datos obtenidos de "scoreboardSaveData" son convertidos a formato JSON y se almacenan en la variable "json"
                stream.Write(json); //Escribe la info de "json" en el archivo ubicado en "SavePath" utilizando StreamWriter.
            }
        }

    }
}


