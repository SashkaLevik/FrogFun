using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Boosters;
using Agava.YandexGames;

namespace Assets.Scripts.UI
{
    public class ScoreCount : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private FrogsCollector _frogsCollector;
        [SerializeField] private SaveLoad _saveLoad;
        [SerializeField] private List<BoosterView> _boosterPrefabs;
        [SerializeField] private Transform _boosterSpawnPos;
        [SerializeField] private BoosterController _target;

        private int _score;
        private int _level;
        private int _maxLevel = 5;
        private int _scoreRiseAmount = 25;
        private int _levelRiseAmount;
        private BoosterView _booster;
        public int Score => _score;
        public int Level => _level;

        public event UnityAction LevelRised;

        private void Start()
        {
            _saveLoad.Load();
            _scoreText.text = _score.ToString();
            _levelRiseAmount = (_level + 1) * 50;
        }

        private void OnEnable()
        {
            _frogsCollector.OnScoreCollected += RiseScore;
        }

        private void OnDisable()
        {
            _frogsCollector.OnScoreCollected += RiseScore;
        }

        public void InitScore(int score)
            => _score = score;

        public void InitLevel(int level)
            => _level = level;

        private void RiseScore()
        {
            _score += _scoreRiseAmount;
            _scoreText.text = _score.ToString();
            
            if (_score  == _levelRiseAmount)
            {
                RiseLevel();
            }

            _saveLoad.Save();
        }

        private void RiseLevel()
        {
            _level++;
            LevelRised?.Invoke();
            Debug.Log("LevelRised");
            //CreateBooster();
            _levelRiseAmount = (_level + 1) * 50;
            ShowInterstitial();
        }

        private void ShowInterstitial()
        {
            InterstitialAd.Show(OnAddOpen, OnAddClose);
        }

        private void OnAddClose(bool obj)
        {
            AudioListener.volume = 1;
            Time.timeScale = 1;
            CreateBooster();
        }

        private void OnAddOpen()
        {
            AudioListener.volume = 0;
            Time.timeScale = 0;
        }

        private void CreateBooster()
        {
            _booster = Instantiate(GetRandomBooster(), _boosterSpawnPos);
            _booster.InitTarget(_target);
        }

        private BoosterView GetRandomBooster()
        {
            return _boosterPrefabs.OrderBy(o => Random.value).First().GetComponent<BoosterView>();
        }
    }
}
