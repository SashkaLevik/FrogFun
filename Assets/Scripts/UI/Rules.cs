using UnityEngine;

namespace Assets.Scripts.UI
{
    public class Rules : MonoBehaviour
    {
        [SerializeField] private GameObject _description;        
        
        public void Show()
        {
            _description.SetActive(true);
            Invoke(nameof(Hide), 5f);
        }

        private void Hide() => _description.SetActive(false);
    }
}
