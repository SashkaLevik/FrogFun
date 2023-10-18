using Assets.Scripts.Platforms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Frog : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Trampoline _trampoline;
        [SerializeField] private BoxCollider2D _lilyCollider;
        [SerializeField] private CircleCollider2D _swampCollider;

        private Rigidbody2D _rigidbody;
        protected float _minScale;
        private float _changeScaleIndex = 1f;
        private float _currentScale;
        private float _velosity;
        private bool _isMoovingUp;
        private bool _isMinScale;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _currentScale = transform.localScale.x;
            _velosity = _rigidbody.velocity.y;

            if (_currentScale <= _minScale) _isMinScale = true;

            if (_velosity > 0)
                _isMoovingUp = true;
            else
                _isMoovingUp = false;

            if (transform.position.y < - 9) Destroy(gameObject);
        }        

        public void InitTrampoline(Trampoline trampoline)
        {
            _trampoline = trampoline;
            _trampoline.SliderChanged += ThrowFrog;
        }

        private void OnDisable()
        {
            _trampoline.SliderChanged -= ThrowFrog;
        }

        public void DisableColliders()
        {
            _lilyCollider.gameObject.SetActive(false);
            _swampCollider.gameObject.SetActive(false);
        }

        public void EnableColliders()
        {
            _lilyCollider.gameObject.SetActive(true);
            _swampCollider.gameObject.SetActive(true);
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        protected void DisableSliderEffect()
        {
            _trampoline.SliderChanged -= ThrowFrog;
            _swampCollider.gameObject.SetActive(true);
        }            

        protected virtual void OnTriggerEnter2D(Collider2D collision) { }             

        protected void EnableFrogCollider()
            => _lilyCollider.gameObject.SetActive(true);

        private IEnumerator ChangeScale()
        {
            yield return new WaitForSeconds(0.1f);

            while (_isMoovingUp == true && _isMinScale == false)
            {
                transform.localScale = new Vector3(transform.localScale.x / _changeScaleIndex, transform.localScale.y / _changeScaleIndex, 0);
                _changeScaleIndex += 0.02f;
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void ThrowFrog(float speed)
        {
            _speed = speed * 750;

            if (_rigidbody != null)
                _rigidbody.AddForce(new Vector2(0, _speed));
            
            StartCoroutine(ChangeScale());
        }        
    }
}


