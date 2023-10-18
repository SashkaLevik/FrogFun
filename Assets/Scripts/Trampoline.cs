using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Trampoline : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        
        private Vector3 _rightBorder = new Vector3(1.65f, -4.4f, 0);
        private Vector3 _leftBorder = new Vector3(-1.65f, -4.4f, 0);

        public event UnityAction<float> SliderChanged;
        public event UnityAction FrogThrowed;

        private void Start()
        {
            StartCoroutine(Move());
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && _slider.value != 0)
            {
                SliderChanged?.Invoke(_slider.value);
                _slider.value = 0;
                FrogThrowed?.Invoke();
            }
        }               

        private IEnumerator Move()
        {
            while (true)
            {
                yield return MoveToTarget(transform, _rightBorder);

                yield return MoveToTarget(transform, _leftBorder);
            }
        }

        private IEnumerator MoveToTarget(Transform obj, Vector3 target)
        {
            while (obj.position != target)
            {
                obj.position = Vector3.MoveTowards(obj.position, target, Time.deltaTime);
                yield return null;
            }            
        }
    }
}
