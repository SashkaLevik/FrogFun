using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Agava.YandexGames;

namespace Assets.Scripts.UI
{
    public class MenuScreen : MonoBehaviour
    {
        private const string Active = "Active";

        [SerializeField] private Button _play;
        [SerializeField] private Button _newGame;
        [SerializeField] private TMP_Text _playText;

        //private void Start()
        //{
        //    if (UnityEngine.PlayerPrefs.HasKey(Active))
        //    {
        //        if (Application.systemLanguage == SystemLanguage.Russian)
        //            _playText.text = "Продолжить";
        //        else if (Application.systemLanguage == SystemLanguage.English)
        //            _playText.text = "Continue";

        //        _newGame.gameObject.SetActive(true);
        //    }
        //}

        private void OnEnable()
        {
            _play.onClick.AddListener(StartGame);
            _newGame.onClick.AddListener(StartNew);
        }

        private void StartNew()
        {
            UnityEngine.PlayerPrefs.DeleteAll();
            //UnityEngine.PlayerPrefs.SetString(Active, "Game");
            SceneManager.LoadScene(2);
        }

        private void StartGame()
        {
            //UnityEngine.PlayerPrefs.SetString(Active, "Game");
            SceneManager.LoadScene(2);
        }
    }
}
