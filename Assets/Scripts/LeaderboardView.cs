using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private PlayerDataElement _playerDataTamplate;
        [SerializeField] private Transform _table;

        private List<PlayerDataElement> _spawnElements = new();

        public void ConstractLeaderboard(List<PlayerData> playerDatas)
        {
            ClearLeaderboard();

            foreach (var data in playerDatas)
            {
                PlayerDataElement playerData = Instantiate(_playerDataTamplate, _table);
                playerData.Initialize(data.Name, data.Rank.ToString(), data.Score.ToString());
                _spawnElements.Add(playerData);
            }
        }

        private void ClearLeaderboard()
        {
            foreach (var entry in _spawnElements)
                Destroy(entry.gameObject);

            _spawnElements = new List<PlayerDataElement>();
        }
    }
}

