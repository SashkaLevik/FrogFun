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

        [SerializeField] private ScoreCount _scoreCount;
        [SerializeField] private BoosterController _booster;

        public void Save()
        {
            PlayerPrefs.SetInt(PlayerScore, _scoreCount.Score);
            PlayerPrefs.SetInt(PlayerLevel, _scoreCount.Level);
            PlayerPrefs.SetInt(Magnet, _booster.MagnetAmount);
            PlayerPrefs.SetInt(Bomb, _booster.BombAmount);
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(PlayerScore))
            {
                _scoreCount.InitScore(PlayerPrefs.GetInt(PlayerScore));
                _scoreCount.InitLevel(PlayerPrefs.GetInt(PlayerLevel));
                _booster.InitMagnet(PlayerPrefs.GetInt(Magnet));
                _booster.InitBomb(PlayerPrefs.GetInt(Bomb));
            }            
        }
    }
}
