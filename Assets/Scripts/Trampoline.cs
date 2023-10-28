using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Trampoline : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        
        private Vector3 _rightBorder = new Vector3(2f, -4.4f, 0);
        private Vector3 _leftBorder = new Vector3(-2f, -4.4f, 0);
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        public event UnityAction<float> SliderChanged;
        public event UnityAction FrogThrowed;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
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
                if (transform.position == _rightBorder) _spriteRenderer.flipX = true;
                yield return MoveToTarget(transform, _leftBorder);
                if (transform.position == _leftBorder) _spriteRenderer.flipX = false;
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
