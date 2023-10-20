using Assets.Scripts.Frogs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Platforms
{
    public class YellowPlatform : Platform
    {
        private int _maxCount = 3;
        private bool _isCollected;
        private List<YellowFrog> _frogs = new List<YellowFrog>();

        public event UnityAction<int> YellowFrogsCollected;

        private void Awake()
        {
            _rightBorder = new Vector3(1.65f, -1f, 0);
            _leftBorder = new Vector3(-1.65f, -1f, 0);
            _startSpeed = 0.5f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out YellowCounter yellowFrog))
            {
                var frog = yellowFrog.GetComponentInParent<YellowFrog>();
                //frog.transform.parent = this.transform;
                _frogs.Add(frog);
                Invoke(nameof(CheckCount), 0.5f);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out YellowCounter yellowFrog) && _isCollected == false)
            {
                var frog = yellowFrog.GetComponentInParent<YellowFrog>();
                //frog.transform.parent = null;
                _frogs.Remove(frog);
            }
        }

        private void CheckCount()
        {            
            if (_frogs.Count >= _maxCount)
            {
                _isCollected = true;
                YellowFrogsCollected?.Invoke(_maxCount);

                foreach (var frog in _frogs)
                    if (frog != null) frog.Die();

                _frogs.Clear();
                _isCollected = false;
            }
        }
    }
}
