using Assets.Scripts.Platforms;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.UI
{
    public class FrogsCollector : MonoBehaviour
    {
        [SerializeField] private TMP_Text _yellowFrogsCount;
        [SerializeField] private TMP_Text _blueFrogsCount;
        [SerializeField] private TMP_Text _redFrogsCount;
        [SerializeField] private YellowPlatform _yellowPlatform;
        [SerializeField] private BluePlatform _bluePlatform;
        [SerializeField] private RedPlatform _redPlatform;

        private int _yellowAmount, _blueAmount, _redAmount;
        private int _maxYellow = 1, _maxBlue = 1, _maxRed = 1;

        public event UnityAction OnScoreCollected; 

        private void OnEnable()
        {
            _yellowPlatform.YellowFrogsCollected += CollectYellow;
            _bluePlatform.BlueFrogsCollected += CollectBlue;
            _redPlatform.RedFrogsCollected += CollectRed;
        }

        private void OnDisable()
        {
            _yellowPlatform.YellowFrogsCollected -= CollectYellow;
            _bluePlatform.BlueFrogsCollected -= CollectBlue;
            _redPlatform.RedFrogsCollected -= CollectRed;
        }

        private void CollectYellow(int frogs)
        {
            _yellowAmount += frogs;

            if (RiseScore(_yellowAmount, _maxYellow))
                _yellowAmount = 0;

            _yellowFrogsCount.text = _yellowAmount.ToString();
        }

        private void CollectBlue(int frogs)
        {
            _blueAmount += frogs;

            if (RiseScore(_blueAmount, _maxBlue))
                _blueAmount = 0;

            _blueFrogsCount.text = _blueAmount.ToString();
        }

        private void CollectRed(int frogs)
        {
            _redAmount += frogs;

            if (RiseScore(_redAmount, _maxRed))
                _redAmount = 0;

            _redFrogsCount.text = _redAmount.ToString();
        }

        private bool RiseScore(int amount, int maxAmount)
        {
            if (amount >= maxAmount)
            {
                OnScoreCollected?.Invoke();
                return true;
            }
            return false;
        }
    }
}
