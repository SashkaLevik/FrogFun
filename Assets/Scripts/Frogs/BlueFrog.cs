using Assets.Scripts.Platforms;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Frogs
{
    public class BlueFrog : Frog
    {        
        private void Awake()
        {
            _minScale = 0.4f;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out DisableBorder border))
                DisableSliderEffect();
            if (collision.TryGetComponent(out BlueLily lily))
            {
                Invoke(nameof(EnableFrogCollider), 0.2f);
            }
        }        
    }
}
