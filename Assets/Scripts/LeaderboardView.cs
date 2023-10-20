using Agava.YandexGames;
using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LeaderboardView : MonoBehaviour
    {
        private const string LeaderboardName = "Leaderboard";
        private const string PlayerScore = "PlayerScore";

        //[SerializeField] private ScoreCount _score;
        [SerializeField] private PlayerData _playerDataTamplate;
        [SerializeField] private Transform _table;        

        private List<PlayerData> _playerDatas = new List<PlayerData>();        

        //private void OnEnable()
        //{
        //    _return.onClick.AddListener(Return);
        //}

        //private void OnDisable()
        //{
        //    _return.onClick.RemoveListener(Return);
        //}

        public void LoadLeaderboard()
        {
            if (YandexGamesSdk.IsInitialized == false)
                return;

            //_leaderboardPanel.SetActive(true);
            Autorize();
            SetScore();
            ClearLeaderboard();

            Agava.YandexGames.Leaderboard.GetEntries(LeaderboardName, (result) =>
            {
                foreach (var entry in result.entries)
                {
                    string name = entry.player.publicName;

                    if (string.IsNullOrEmpty(name))
                        name = "Anonymus";

                    PlayerData playerData = Instantiate(_playerDataTamplate, _table);
                    playerData.Initialize(name, entry.rank, entry.score);
                    _playerDatas.Add(playerData);
                }
            });
        }

        private void Autorize()
        {
            PlayerAccount.RequestPersonalProfileDataPermission();

            if (PlayerAccount.IsAuthorized == false)
                PlayerAccount.Authorize();
        }

        private void SetScore()
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, UnityEngine.PlayerPrefs.GetInt(PlayerScore));//ScoreCount._score);
        }

        private void ClearLeaderboard()
        {
            foreach (PlayerData entry in _playerDatas)
                Destroy(entry.gameObject);

            _playerDatas = new List<PlayerData>();
        }

        //private void Return()
        //{
        //    _leaderboardPanel.SetActive(false);
        //}
    }
}
