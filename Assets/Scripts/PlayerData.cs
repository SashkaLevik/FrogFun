using System;


namespace Assets.Scripts
{
    [Serializable]
    public class PlayerData
    {
        public int Level;
        public int Score;

        public PlayerData()
        {
            Level = 0;
            Score = 0;
        }
    }    
}
