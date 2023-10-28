using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class SwampCapacityCheck : MonoBehaviour
    {
        private const string IsYellowDanger = "IsYellowDanger";
        private const string IsRedDanger = "IsRedDanger";

        [SerializeField] private TMP_Text _count;
        [SerializeField] private Image _looseScreen;

        private Animator _animator;
        private int _yellowDanger = 15;
        private int _redDanger = 5;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            Time.timeScale = 1;
        }

        public void ChekCapacity(int capacity)
        {
            if (capacity <= 0)
            {                
                _looseScreen.gameObject.SetActive(true);
                Invoke(nameof(StopTime), 0.7f);
            }
            else if (capacity <= _yellowDanger && capacity > _redDanger)
            {
                _animator.SetBool(IsYellowDanger, true);
                _animator.SetBool(IsRedDanger, false);
                _count.color = Color.yellow;
            }
            else if (capacity <= _redDanger)
            {
                _animator.SetBool(IsYellowDanger, false);
                _animator.SetBool(IsRedDanger, true);
                _count.color = Color.red;
            }            
            else
            {
                _animator.SetBool(IsYellowDanger, false);
                _animator.SetBool(IsRedDanger, false);
                _count.color = Color.white;
            }
        }

        public void StopTime() => Time.timeScale = 0;
    }
}
