using System;

namespace GasHero.Scoreboard
{
    //Esta estructura recibe los datos ingresados para guardarlos

    [Serializable]
    public struct ScoreboardEntryData // Se utiliza una estructura en lugar de una clase porque ofrece la posibilidad de combinar
                                      // datos de diferentes tipos para crear un nuevo tipo compuesto, datos que tienen un vinculo entre si.                                       
    {
        public string entryName;
        public int entryScore;        

    }
}


