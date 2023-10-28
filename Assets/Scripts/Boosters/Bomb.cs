using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Boosters
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private Transform _collidePoint;
        [SerializeField] private float _radius;
        [SerializeField] private Trampoline _trampoline;
        [SerializeField] private LayerMask _frogLayer;
        [SerializeField] private CircleCollider2D _swampCollider;
        [SerializeField] private AudioSource _explode;
        [SerializeField] private GameObject _boomAnim;

        public Collider2D[] _frogs;
        private Rigidbody2D _rigidbody;
        private float _speed;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            //Invoke(nameof(Explode), 4f);
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out DisableBorder border))
            {
                EnableSwampTrigger();
                StartCoroutine(Explode());
            }
        }
        
        private void EnableSwampTrigger()
        {
            _swampCollider.gameObject.SetActive(true);
            _trampoline.SliderChanged -= ThrowBomb;
        }

        public void InitTrampoline(Trampoline trampoline)
        {
            _trampoline = trampoline;
            _trampoline.SliderChanged += ThrowBomb;
        }

        private void ThrowBomb(float speed)
        {
            _speed = speed * 750;

            if (_rigidbody != null)
                _rigidbody.AddForce(new Vector2(0, _speed));
        }

        private void FindFrogs()
            => _frogs = Physics2D.OverlapCircleAll(transform.position, _radius, _frogLayer);

        //private void Explode()
        //{
        //    _frogs = Physics2D.OverlapCircleAll(transform.position, _radius, _frogLayer);

        //    for (int i = 0; i < _frogs.Length; i++)
        //    {
        //        if (_frogs[i] != null)
        //        {
        //            _frogs[i].GetComponent<Frog>().Die();
        //        }
        //    }
        //}

        private IEnumerator Explode()
        {
            yield return new WaitForSeconds(2f);
            FindFrogs();
            Instantiate(_boomAnim, transform.position, Quaternion.identity);
            _explode.Play();
            //Collider2D[] frogs = Physics2D.OverlapCircleAll(transform.position, _radius, _frogLayer);

            foreach (var frog in _frogs)
            {
                if (frog != null) frog.GetComponent<Frog>().Die();
            }
            yield return new WaitForSeconds(0.7f);
            DestroyBomb();
            //Invoke(nameof(DestroyBomb), 0.3f);
        }

        private void DestroyBomb() => Destroy(gameObject);
    }
}
