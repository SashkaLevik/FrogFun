using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Logo : MonoBehaviour
    {
        [SerializeField] private AudioSource _buttons;
        [SerializeField] private GameObject _logoPanel;       

        public void PlayButtons()
            => _buttons.Play();

        public void HideLogo()
        {
            SceneManager.LoadScene(1);
            //_logoPanel.SetActive(false);
        }
    }
}
