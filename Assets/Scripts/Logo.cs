using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Logo : MonoBehaviour
    {
        [SerializeField] private AudioSource _buttons;
        [SerializeField] private GameObject _logoPanel;

        public void LoadMenu() => Invoke(nameof(HideLogo), 1.5f);

        public void PlayButtons() => _buttons.Play();

        private void HideLogo() => SceneManager.LoadScene(1);
    }
}
