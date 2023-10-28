using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class YandexInitialization : MonoBehaviour
    {
        [SerializeField] private Logo _logo;

        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif
            YandexGamesSdk.CallbackLogging = true;
            yield return YandexGamesSdk.Initialize(_logo.LoadMenu);            
        }        
    }
}
