using Assets.Scripts.Frogs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Platforms
{
    public class RedPlatform : Platform
    {
        private int _maxCount = 3;
        private bool _isCollected;
        private List<RedFrog> _frogs = new List<RedFrog>();

        public event UnityAction<int> RedFrogsCollected;

        private void Awake()
        {
            _rightBorder = new Vector3(1.65f, 1.7f, 0);
            _leftBorder = new Vector3(-1.65f, 1.7f, 0);
            _startSpeed = 0.7f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out RedCounter redFrog))
            {
                var frog = redFrog.GetComponentInParent<RedFrog>();
                //frog.transform.parent = this.transform;
                _frogs.Add(frog);
                Invoke(nameof(CheckCount), 0.5f);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out RedCounter redFrog) && _isCollected == false)
            {
                var frog = redFrog.GetComponentInParent<RedFrog>();
                //frog.transform.parent = null;
                _frogs.Remove(frog);
            }
        }

        private void CheckCount()
        {
            if (_frogs.Count >= _maxCount)
            {
                _isCollected = true;
                RedFrogsCollected?.Invoke(_maxCount);

                foreach (var frog in _frogs)
                    if (frog != null) frog.Die();

                _frogs.Clear();
                _isCollected = false;
            }
        }
    }
}
