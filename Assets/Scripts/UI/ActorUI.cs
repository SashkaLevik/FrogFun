﻿using UnityEngine;
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
        [SerializeField] private Button _sound;
        [SerializeField] private Image _defaultImage;
        [SerializeField] private Sprite _mute;
        [SerializeField] private Sprite _soundOn;
        
        private void Start()
        {
            _swampCapacity.text = _swamp.CurrentCapacity.ToString();            
        }

        private void OnEnable()
        {
            _sound.onClick.AddListener(ChangeSound);
            _swamp.CapacityChanged += ChangeSwampCapacity;
            _menu.onClick.AddListener(ReturnToMenu);
            WebApplication.InBackgroundChangeEvent += OnBackgroundChange;
        }        

        private void OnDisable()
        {
            _sound.onClick.RemoveListener(ChangeSound);
            _swamp.CapacityChanged -= ChangeSwampCapacity;
            _menu.onClick.RemoveListener(ReturnToMenu);
            WebApplication.InBackgroundChangeEvent -= OnBackgroundChange;
        }

        private void ChangeSound()
        {
            if (AudioListener.volume == 1)
            {
                AudioListener.volume = 0;
                _defaultImage.sprite = _mute;
            }
            else if (AudioListener.volume == 0)
            {
                AudioListener.volume = 1;
                _defaultImage.sprite = _soundOn;
            }
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
