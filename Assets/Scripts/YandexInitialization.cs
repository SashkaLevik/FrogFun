using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class YandexInitialization : MonoBehaviour
    {
        private const string LeaderboardName = "Name";

        [SerializeField] private Localization _localization;
        [SerializeField] private LeaderboardView _leaderboardView;

        public event UnityAction PlayerAuthorized;
        public event UnityAction Completed;

        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            yield return YandexGamesSdk.Initialize(() => PlayerAccount.RequestPersonalProfileDataPermission());

            Completed?.Invoke();

            Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
            {
                if (result != null)
                    PlayerAuthorized?.Invoke();
            });

            OnInitialized();
        }

        private void OnInitialized()
        {
            _localization.SetLanguage(YandexGamesSdk.Environment.i18n.lang);
            _leaderboardView.LoadLeaderboard();
        }
    }
}
