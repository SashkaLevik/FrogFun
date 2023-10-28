using Assets.Scripts.UI;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Platforms
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] protected ScoreCount _score;

        private float _maxSpeed = 1f;
        protected Vector3 _rightBorder;
        protected Vector3 _leftBorder;
        protected float _speed;
        protected float _startSpeed;
        protected float _speedRiseIndex = 0.1f;
        protected int _frogsCount;


        private void Start()
        {
            if (_score.Level > 0)
            {
                _startSpeed += _score.Level * _speedRiseIndex;
            }
            if(_speed < _maxSpeed) _speed = _startSpeed;

            StartCoroutine(Move());
        }

        private void OnEnable()
        {
            _score.LevelRised += IncreaseSpeed;
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
                obj.position = Vector3.MoveTowards(obj.position, target, Time.deltaTime * _speed);
                yield return null;
            }
        }

        protected void IncreaseSpeed()
        {
            if (_speed < _maxSpeed)
            {
                _speed = _startSpeed + (_score.Level * _speedRiseIndex);
            }
        }
    }
}
