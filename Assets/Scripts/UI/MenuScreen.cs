using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.UI
{
    public class MenuScreen : MonoBehaviour
    {
        private const string Active = "Active";

        [SerializeField] private Button _play;
        [SerializeField] private Button _newGame;
        [SerializeField] private TMP_Text _playText;

        private void Start()
        {
            if (PlayerPrefs.HasKey(Active))
            {
                _playText.text = "Continue";
                _newGame.gameObject.SetActive(true);
            }
        }

        private void OnEnable()
        {
            _play.onClick.AddListener(StartGame);
            _newGame.onClick.AddListener(StartNew);
        }

        private void StartNew()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString(Active, "Game");
            SceneManager.LoadScene(1);
        }

        private void StartGame()
        {
            PlayerPrefs.SetString(Active, "Game");
            SceneManager.LoadScene(1);
        }
    }
}
