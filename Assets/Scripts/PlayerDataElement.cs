using TMPro;
using UnityEngine;


namespace Assets.Scripts
{
    public class PlayerDataElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _rank;
        [SerializeField] private TMP_Text _score;

        public void Initialize(string name, string rank, string score)
        {
            _name.text = name;
            _rank.text = rank;
            _score.text = score;
        }
    }    

    public class PlayerData
    {
        public string Name { get; private set; }
        public int Score { get; private set; }
        public int Rank { get; private set; }

        public PlayerData(string name, int score, int rank)
        {
            Name = name;
            Score = score;
            Rank = rank;
        }
    }
}
