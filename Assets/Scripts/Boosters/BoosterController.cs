
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Boosters
{
    public class BoosterController : MonoBehaviour
    {
        [SerializeField] private Button _magnetButton;
        [SerializeField] private Button _bombButton;
        [SerializeField] private TMP_Text _magnetText;
        [SerializeField] private TMP_Text _bombText;
        [SerializeField] private Magnet _magnet;
        [SerializeField] private SaveLoad _saveLoad;

        private int _magnetAmount = 1;
        private int _bombAmount = 1;

        public int MagnetAmount => _magnetAmount;
        public int BombAmount => _bombAmount;

        public event UnityAction BombCreated;

        private void Start()
        {
            _saveLoad.Load();
            _magnetText.text = _magnetAmount.ToString();
            _bombText.text = _bombAmount.ToString();
        }

        private void OnEnable()
        {
            _magnetButton.onClick.AddListener(SetMagnet);
            _bombButton.onClick.AddListener(SetBomb);
        }

        private void OnDisable()
        {
            _magnetButton.onClick.RemoveListener(SetMagnet);
            _bombButton.onClick.RemoveListener(SetBomb);
        }

        public void InitMagnet(int amount)
            => _magnetAmount = amount;

        public void InitBomb(int amount)
            => _bombAmount = amount;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out MagnetView magnet))
            {
                _magnetAmount++;
                _magnetText.text = _magnetAmount.ToString();
                Destroy(magnet.gameObject);
            }

            if (collision.TryGetComponent(out BombView bomb))
            {
                _bombAmount++;
                _bombText.text = _bombAmount.ToString();
                Destroy(bomb.gameObject);
            }

            _saveLoad.Save();
        }

        private void SetMagnet()
        {
            if (_magnetAmount > 0)
            {
                Instantiate(_magnet);
                _magnetAmount--;
                _magnetText.text = _magnetAmount.ToString();
                _saveLoad.Save();
            }
        }            

        private void SetBomb()
        {
            if(_bombAmount > 0)
            {
                BombCreated?.Invoke();
                _bombAmount--;
                _bombText.text = _bombAmount.ToString();
                _saveLoad.Save();
            }
        }        
    }
}
