using Assets.Scripts.Boosters;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class FrogSpawner : MonoBehaviour
    {
        private const string Frogs = "Frog";

        [SerializeField] private Trampoline _trampoline;
        [SerializeField] private Bomb _bomb;
        [SerializeField] private BoosterController _booster;

        private Frog _frog;
        private List<Frog> _frogs;

        private void Awake()
            => _frogs = Resources.LoadAll<Frog>(Frogs).ToList();

        private void Start()
            => SpawnFrog();                

        private void OnEnable()
        {
            _trampoline.FrogThrowed += Spawn;
            _booster.BombCreated += SetBomb;
        }            

        private void OnDisable()
        {
            _trampoline.FrogThrowed -= Spawn;
            _booster.BombCreated -= SetBomb;
        }

        public void FillSwamp(Transform swamp)
        {
            _frog = Instantiate(GetRandomFrog(), swamp.position, Quaternion.identity);
            _frog.EnableColliders();
            _frog.InitTrampoline(_trampoline);
            _frog.StartChangeScale();
        }

        private void Spawn()
            => Invoke(nameof(SpawnFrog), 0.6f);

        private void SpawnFrog()
        {
            _frog = Instantiate(GetRandomFrog(), transform.position, Quaternion.identity);
            _frog.InitTrampoline(_trampoline);
        }

        private void SetBomb()
            => Invoke(nameof(CreateBomb), 0.3f);

        private void CreateBomb()
        {
            if (_frog != null)
            {
                Destroy(_frog.gameObject);
                var bomb = Instantiate(_bomb, transform.position, Quaternion.identity);
                bomb.InitTrampoline(_trampoline);
            }
        }

        private Frog GetRandomFrog()
        {
            return _frogs.OrderBy(o => Random.value).First();
        }
    }
}
