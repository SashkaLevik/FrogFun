using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Boosters
{
    public class BoosterView : MonoBehaviour
    {
        [SerializeField] private BoosterController _target;

        private float _speed = 3f;

        private void Start()
        {
            StartCoroutine(Move());
        }

        public void InitTarget(BoosterController target)
            => _target = target;

        //public void MoveToTarget(Transform target)
        //    => StartCoroutine(Move(target));

        private IEnumerator Move()
        {
            yield return new WaitForSeconds(0.4f);

            while (transform.position != _target.transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed);
                yield return null;
            }            
        }
    }
}
