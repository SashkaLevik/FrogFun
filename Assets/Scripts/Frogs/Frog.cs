using Assets.Scripts.Platforms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Frog : MonoBehaviour
    {
        private const string Croak = "Croak";

        [SerializeField] private float _speed;
        [SerializeField] private Trampoline _trampoline;
        [SerializeField] private BoxCollider2D _lilyCollider;
        [SerializeField] private CircleCollider2D _swampCollider;
        [SerializeField] protected List<AudioSource> _sounds;
        [SerializeField] private GameObject _boomAnim;

        private Animator _animator;
        public float _randomNumber;
        public float _time;
        protected AudioSource _croakSound;
        private Rigidbody2D _rigidbody;
        protected float _minScale;
        private float _changeScaleIndex = 1f;
        private float _currentScale;
        protected SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            StartCoroutine(PlayCroak());
        }

        private void Update()
        {
            _currentScale = transform.localScale.x;

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

        public void ThrowFrog(float speed)
        {
            _speed = speed * 750;

            if (_rigidbody != null)
                _rigidbody.AddForce(new Vector2(0, _speed));

            StartChangeScale();
        }

        public void StartChangeScale() => StartCoroutine(ChangeScale());

        public void DisableColliders()
        {
            _lilyCollider.gameObject.SetActive(false);
            _swampCollider.gameObject.SetActive(false);
        }

        public void EnableColliders()
        {
            _lilyCollider.gameObject.SetActive(true);
            _swampCollider.gameObject.SetActive(true);
            _spriteRenderer.sortingOrder = 2;
        }

        public void Die()
        {
            Instantiate(_boomAnim, transform.position, Quaternion.identity);
            Invoke(nameof(DestroyFrog), 0.7f);
        }

        protected void DisableSliderEffect()
        {
            _trampoline.SliderChanged -= ThrowFrog;
            _swampCollider.gameObject.SetActive(true);
            _spriteRenderer.sortingOrder = 2;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision) { }             

        protected void EnableFrogCollider()
            => _lilyCollider.gameObject.SetActive(true);

        private AudioSource GetRandomSound()
        {
            return _sounds.OrderBy(o => Random.value).First();
        }

        private IEnumerator ChangeScale()
        {
            yield return new WaitForSeconds(0.1f);

            while (_currentScale >= _minScale)
            {
                transform.localScale = new Vector3(transform.localScale.x / _changeScaleIndex, transform.localScale.y / _changeScaleIndex, 0);
                _changeScaleIndex += 0.02f;
                yield return new WaitForSeconds(0.1f);
            }

            //while (_isMoovingUp == true && _isMinScale == false)
            //{
            //    transform.localScale = new Vector3(transform.localScale.x / _changeScaleIndex, transform.localScale.y / _changeScaleIndex, 0);
            //    _changeScaleIndex += 0.02f;
            //    yield return new WaitForSeconds(0.1f);
            //}
        }

        private IEnumerator PlayCroak()
        {
            _croakSound = GetRandomSound();
            _randomNumber = Random.Range(3, 6);
            _time = 0;

            while (true)
            {                                
                _time += Time.deltaTime;

                if (_time >= _randomNumber)
                {
                    _animator.SetTrigger(Croak);
                    _croakSound.Play();
                    _time = 0;
                }
                yield return null;
            }
        }

        private void DestroyFrog() => Destroy(gameObject);
    }
}


