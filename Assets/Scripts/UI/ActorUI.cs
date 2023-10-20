using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Agava.WebUtility;

namespace Assets.Scripts.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private Swamp _swamp;
        [SerializeField] private SwampCapacityCheck _capacityCheck;
        [SerializeField] private TMP_Text _swampCapacity;
        [SerializeField] private Button _menu;
        [SerializeField] private AudioSource _mainTheme;

        private int _maxCapacity = 30;
        
        private void Start()
        {
            _swampCapacity.text = _maxCapacity.ToString();            
        }

        private void OnEnable()
        {
            _swamp.CapacityChanged += ChangeSwampCapacity;
            _menu.onClick.AddListener(ReturnToMenu);
            WebApplication.InBackgroundChangeEvent += OnBackgroundChange;
        }        

        private void OnDisable()
        {
            _swamp.CapacityChanged -= ChangeSwampCapacity;
            _menu.onClick.RemoveListener(ReturnToMenu);
            WebApplication.InBackgroundChangeEvent -= OnBackgroundChange;
        }

        private void OnBackgroundChange(bool inBackground)
        {
            AudioListener.pause = inBackground;
            AudioListener.volume = inBackground ? 0f : 1f;
        }

        private void ReturnToMenu()
        {
            SceneManager.LoadScene(1);
        }

        private void ChangeSwampCapacity(int capacity)
        {
            _swampCapacity.text = capacity.ToString();
            _capacityCheck.ChekCapacity(capacity);
        }
    }
}
