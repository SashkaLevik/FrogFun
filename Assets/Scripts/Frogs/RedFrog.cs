using Assets.Scripts.Platforms;
using UnityEngine;

namespace Assets.Scripts.Frogs
{
    public class RedFrog : Frog
    {
        private void Awake()
        {
            _minScale = 0.06f;
        }
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out DisableBorder border))
                DisableSliderEffect();
            if (collision.TryGetComponent(out RedLily lily))
            {
                Invoke(nameof(EnableFrogCollider), 0.2f);
            }
        }
    }
}
