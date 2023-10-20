using Lean.Localization;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Localization : MonoBehaviour
    {
        [SerializeField] private LeanLocalization _leanLocalization;

        private Dictionary<string, string> _language = new()
        {
            { "ru", "Russian" },
            { "en", "English" },
        };

        public void SetLanguage(string value)
        {
            if (_language.ContainsKey(value))
                _leanLocalization.SetCurrentLanguage(_language[value]);
        }
    }
}
