using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

namespace Assets.Scripts.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private Swamp _swamp;
        [SerializeField] private SwampCapacityCheck _capacityCheck;
        [SerializeField] private TMP_Text _swampCapacity;
        [SerializeField] private Button _menu;

        private int _maxCapacity = 30;
        

        private void Start()
        {
            _swampCapacity.text = _maxCapacity.ToString();
        }

        private void OnEnable()
        {
            _swamp.CapacityChanged += ChangeSwampCapacity;
            _menu.onClick.AddListener(ReturnToMenu);
        }               

        private void OnDisable()
        {
            _swamp.CapacityChanged -= ChangeSwampCapacity;
            _menu.onClick.RemoveListener(ReturnToMenu);
        }

        private void ReturnToMenu()
        {
            SceneManager.LoadScene(0);
        }

        private void ChangeSwampCapacity(int capacity)
        {
            _swampCapacity.text = capacity.ToString();
            _capacityCheck.ChekCapacity(capacity);
        }
    }
}
