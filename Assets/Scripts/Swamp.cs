using Assets.Scripts.Frogs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class Swamp : MonoBehaviour
    {
        private List<Frog> _frogs = new List<Frog>();
        private int _swampCapacity = 30;
        

        public event UnityAction<int> CapacityChanged;

        private void Start()
        {
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Frog frog))
            {
                _frogs.Add(frog);
                _swampCapacity--;
                CapacityChanged?.Invoke(_swampCapacity);

                //var frogInSwamp = frog.GetComponent<Frog>();
                //Invoke(nameof(CheckCapacity), 0.5f);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Frog frog))
            {
                _frogs.Remove(frog);
                _swampCapacity++;
                CapacityChanged?.Invoke(_swampCapacity);
            }
        }

        private void CheckCapacity()
        {
            
        }
    }
}
