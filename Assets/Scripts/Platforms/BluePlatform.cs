using Assets.Scripts.Frogs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Platforms
{
    public class BluePlatform : Platform
    {
        private int _maxCount = 2;
        private bool _isCollected;
        private List<BlueFrog> _frogs = new List<BlueFrog>();

        public event UnityAction<int> BlueFrogsCollected;

        private void Awake()
        {
            _rightBorder = new Vector3(1.65f, 0.8f, 0);
            _leftBorder = new Vector3(-1.65f, 0.8f, 0);
            _startSpeed = 0.7f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out BlueCounter blueFrog))
            {
                var frog = blueFrog.GetComponentInParent<BlueFrog>();
                frog.transform.parent = this.transform;
                _frogs.Add(frog);
                _frogsCount++;
                Invoke(nameof(CheckCount), 0.5f);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out BlueCounter blueFrog) && _isCollected == false)
            {
                var frog = blueFrog.GetComponentInParent<BlueFrog>();
                _frogs.Remove(frog);
                _frogsCount--;
            }
        }

        private void CheckCount()
        {
            if (_frogs.Count >= _maxCount)
            {
                _isCollected = true;
                BlueFrogsCollected?.Invoke(_maxCount);

                foreach (var frog in _frogs)
                    if (frog != null) frog.Die();

                _frogs.Clear();
                _isCollected = false;
            }
        }
    }
}
