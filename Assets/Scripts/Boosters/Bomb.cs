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

        public Collider2D[] _frogs;
        private Rigidbody2D _rigidbody;
        private float _speed;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();            
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out DisableBorder border))
            {
                EnableSwampTrigger();
                StartCoroutine(Explode());
                //Invoke(nameof(Destroy), 3.2f);
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

        private IEnumerator Explode()
        {
            yield return new WaitForSeconds(3f);
            FindFrogs();

            foreach (var frog in _frogs)
            {
                Destroy(frog.gameObject);
            }
        }

        private void Destroy()
            => Destroy(gameObject);
    }
}
