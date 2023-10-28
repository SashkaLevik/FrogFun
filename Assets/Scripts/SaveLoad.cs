using Assets.Scripts.Boosters;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class SaveLoad : MonoBehaviour
    {
        private const string PlayerLevel = "PlayerLevel";
        private const string PlayerScore = "PlayerScore";
        private const string Magnet = "Magnet";
        private const string Bomb = "Bomb";
        private const string Frogs = "Frogs";
        private const string Yellow = "YellowFrog";
        private const string Blue = "BlueAmount";
        private const string Red = "RedAmount";

        [SerializeField] private ScoreCount _scoreCount;
        [SerializeField] private BoosterController _booster;
        [SerializeField] private Swamp _swamp;
        [SerializeField] private FrogsCollector _frogsCollector;

        public void Save()
        {
            PlayerPrefs.SetInt(PlayerScore, _scoreCount.Score);
            PlayerPrefs.SetInt(PlayerLevel, _scoreCount.Level);
            PlayerPrefs.SetInt(Magnet, _booster.MagnetAmount);
            PlayerPrefs.SetInt(Bomb, _booster.BombAmount);
            PlayerPrefs.SetInt(Frogs, _swamp.FrogsInSwamp);
            PlayerPrefs.SetInt(Yellow, _frogsCollector.YellowAmount);
            PlayerPrefs.SetInt(Blue, _frogsCollector.BlueAmount);
            PlayerPrefs.SetInt(Red, _frogsCollector.RedAmount);
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(Frogs))
            {
                _scoreCount.InitScore(PlayerPrefs.GetInt(PlayerScore));
                _scoreCount.InitLevel(PlayerPrefs.GetInt(PlayerLevel));
                _booster.InitMagnet(PlayerPrefs.GetInt(Magnet));
                _booster.InitBomb(PlayerPrefs.GetInt(Bomb));
                _swamp.InitCapacity(PlayerPrefs.GetInt(Frogs));
                _frogsCollector.InitYellow(PlayerPrefs.GetInt(Yellow));
                _frogsCollector.InitBlue(PlayerPrefs.GetInt(Blue));
                _frogsCollector.InitRed(PlayerPrefs.GetInt(Red));
            }
        }
    }
}
