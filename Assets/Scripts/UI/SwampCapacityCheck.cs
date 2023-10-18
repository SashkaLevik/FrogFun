using UnityEngine;

namespace Assets.Scripts.UI
{
    public class SwampCapacityCheck : MonoBehaviour
    {
        private Animator _animator;
        private int _yellowDanger = 15;
        private int _redDanger = 5;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void ChekCapacity(int capacity)
        {
            if (capacity <= _yellowDanger && capacity > _redDanger)
            {
                _animator.SetBool("IsYellowDanger", true);
                _animator.SetBool("IsRedDanger", false);
            }
            else if (capacity <= _redDanger)
            {
                _animator.SetBool("IsYellowDanger", false);
                _animator.SetBool("IsRedDanger", true);
            }
            else
            {
                _animator.SetBool("IsYellowDanger", false);
                _animator.SetBool("IsRedDanger", false);
            }
        }
    }
}
