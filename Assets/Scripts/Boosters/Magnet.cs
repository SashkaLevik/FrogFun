using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Boosters
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField] private GameObject _magnet;
        [SerializeField] private Transform _collidePoint;
        [SerializeField] private float _rangeX;
        [SerializeField] private float _rangeY;
        [SerializeField] private LayerMask _frogLayer;
        [SerializeField] private AudioSource _atttaction;

        private float _pullSpeed = 525f;
        public Collider2D[] _frogs;

        private void Start()
        {
            FindFrogs();
            _atttaction.Play();
            Invoke(nameof(Destroy), 2f);
        }        

        private void FindFrogs()
        {
            _frogs = Physics2D.OverlapBoxAll(_collidePoint.position, new Vector2(_rangeX, _rangeY), 0, _frogLayer);            

            StartCoroutine(PullFrogs());
        }        

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Frog frog))
            {
                frog.EnableColliders();
            }
        }

        private IEnumerator PullFrogs()
        {            
            yield return new WaitForSeconds(0.2f);

            foreach (var frog in _frogs)
            {
                if (frog != null)
                {
                    var frogToPull = frog.GetComponent<Frog>();
                    frogToPull.DisableColliders();
                    frogToPull.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _pullSpeed));
                }
            }               
        }                   

        private void Destroy()
            => Destroy(gameObject);
    }
}
