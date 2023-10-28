using Agava.YandexGames;
using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class YandexLeaderboardView : MonoBehaviour
    {
        private const string Leaderboard = "Leaderboard";
        private const string PlayerScore = "PlayerScore";

        [SerializeField] private Localization _localization;
        
        [SerializeField] private Button _leaderboard;
        [SerializeField] private Button _close;
        [SerializeField] private Button _closeAutorize;
        [SerializeField] private Button _autorize;
        [SerializeField] private GameObject _leaderboardPanel;
        [SerializeField] private GameObject _autorizePanel;
        [SerializeField] private LeaderboardView _leaderboardView;

        private List<PlayerDataElement> _playerDatas = new List<PlayerDataElement>();

        private void Start()
        {
            OnInitialized();
        }

        private void OnEnable()
        {
            _leaderboard.onClick.AddListener(LoadLeaderboard);
            _autorize.onClick.AddListener(Autorize);
            _close.onClick.AddListener(CloseLeaderboard);
            _closeAutorize.onClick.AddListener(CloseAutorize);
        }

        private void OnDisable()
        {
            _leaderboard.onClick.RemoveListener(LoadLeaderboard);
            _autorize.onClick.RemoveListener(Autorize);
            _close.onClick.RemoveListener(CloseLeaderboard);
            _closeAutorize.onClick.RemoveListener(CloseAutorize);
        }

        private bool CheckAutorize()
            => PlayerAccount.IsAuthorized;

        public void LoadLeaderboard()
        {
            if (CheckAutorize())
            {
                PlayerAccount.RequestPersonalProfileDataPermission();
                _leaderboardPanel.SetActive(true);
                SetScore();
                CreateLeaderboard();
            }
            else
            {
                _autorizePanel.SetActive(true);
            }
        }

        private void CreateLeaderboard()
        {
            List<PlayerData> playersData = new();

            Agava.YandexGames.Leaderboard.GetEntries(Leaderboard, (result) =>
            {
                foreach (var entry in result.entries)
                {
                    string name = entry.player.publicName;

                    if (string.IsNullOrEmpty(name))
                        name = "Anonymus";
                   
                    playersData.Add(new PlayerData(name, entry.score, entry.rank));
                }

                _leaderboardView.ConstractLeaderboard(playersData);
            });
        }       

        private void Autorize()
        {
            if (PlayerAccount.IsAuthorized == false)
            {
                PlayerAccount.Authorize();
                _autorizePanel.SetActive(false);
            }
        }

        private void SetScore()
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            Agava.YandexGames.Leaderboard.SetScore(Leaderboard, UnityEngine.PlayerPrefs.GetInt(PlayerScore));
        }

        //private void ClearLeaderboard()
        //{
        //    foreach (PlayerDataElement entry in _playerDatas)
        //        Destroy(entry.gameObject);

        //    _playerDatas = new List<PlayerDataElement>();
        //}

        private void OnInitialized()
        {
            _localization.SetLanguage(YandexGamesSdk.Environment.i18n.lang);
            //YandexGamesSdk.GameReady();
        }

        private void CloseLeaderboard() => _leaderboardPanel.SetActive(false);
        private void CloseAutorize() => _autorizePanel.SetActive(false);
    }
}

