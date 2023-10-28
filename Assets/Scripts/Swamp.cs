using Assets.Scripts.Frogs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class Swamp : MonoBehaviour
    {
        [SerializeField] private SaveLoad _saveLoad;
        [SerializeField] private FrogSpawner _frogSpawner;
        [SerializeField] private Transform _fillPos;

        private List<Frog> _frogs = new List<Frog>();
        private int _maxCapacity = 30;
        private int _currentCapacity;
        private int _frogsInSwamp;

        public int CurrentCapacity => _currentCapacity;
        public int FrogsInSwamp => _frogsInSwamp;

        public event UnityAction<int> CapacityChanged;

        private void Awake()
        {
            _saveLoad.Load();
            _currentCapacity = _maxCapacity;
        }

        private void Start()
        {            
            if (_frogsInSwamp > 0)
            {
                for (int i = 0; i < _frogsInSwamp; i++)
                {
                    _frogSpawner.FillSwamp(_fillPos);
                }
            }            
        }

        public void InitCapacity(int frogs)
            => _frogsInSwamp = frogs;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Frog frog))
            {
                StartCoroutine(CheckCapacity(frog));
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Frog frog))
            {
                _frogs.Remove(frog);
                _currentCapacity++;
                _frogsInSwamp = _maxCapacity - _currentCapacity;
                CapacityChanged?.Invoke(_currentCapacity);
                _saveLoad.Save();
            }
        }

        private IEnumerator CheckCapacity(Frog frog)
        {
            yield return new WaitForSeconds(0.5f);

            _frogs.Add(frog);
            _currentCapacity--;
            _frogsInSwamp = _maxCapacity - _currentCapacity;
            CapacityChanged?.Invoke(_currentCapacity);
            _saveLoad.Save();
        }
    }
}
