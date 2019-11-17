using System.Collections.Generic;
using Assets.Scripts.Core.Pools;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        private static Game _instance;
        private Player _player;
        private List<EnemyController> _enemies = new List<EnemyController>();
        private GameObjectPool<EnemyController> _enemiesPool;
        private GameObjectPool<ProgressBar> _enemiesHPPool;
        
        [SerializeField]
        private GameObject _container;
        [SerializeField]
        private EnemyController _enemyPrefab;
        [SerializeField]
        private ProgressBar _enemyHPPrefab;

        public static Game Instance
        {
            get { return _instance ?? (_instance = Object.FindObjectOfType<Game>()); }
        }

        private void Awake()
        {
            _enemiesPool = new GameObjectPool<EnemyController>(_container, _enemyPrefab, 5);
            _enemiesHPPool = new GameObjectPool<ProgressBar>(_container, _enemyHPPrefab, 5);
        }

        public void StartGame()
        {
            SpawnBall();
            SpawnEnemies();
        }

        private void SpawnBall()
        {
            var gokiObj = GameObject.Find("Ball");
            var model = AlliesCharacterFactory.Create(AllyCharacterType.AllyType0, 1);
            gokiObj.GetComponent<AllyController>().SetCharacter(model);
        }

        private void SpawnEnemies()
        {
            for (int i = 0; i < 5; i++)
            {
                var enemy = _enemiesPool.GetObject();
                enemy.transform.SetParent(transform);
                enemy.transform.position = new Vector3(Random.Range(-2f,2f), Random.Range(-2f, 3f));
                var model = EnemiesCharacterFactory.Create(EnemyCharacterType.EnemyType0, 1);
                enemy.SetCharacter(model);
                enemy.gameObject.SetActive(true);
                _enemies.Add(enemy);
                var hpBar = _enemiesHPPool.GetObject();
                hpBar.transform.SetParent(DragController.Instance.Canvas.transform);
                hpBar.transform.localScale = Vector3.one;
                hpBar.transform.position = DragController.Instance.GetCanvasPosition(enemy.transform.position + new Vector3(0, enemy.HPBarOffset));
                hpBar.gameObject.SetActive(true);
                hpBar.SetValue(1, true);

                void OnHealthOnChanged(CharacterStat s) => hpBar.SetValue(s.Progress);

                enemy.Health.Changed += OnHealthOnChanged;

                void OnEnemyOnDeath()
                {
                    enemy.Health.Changed -= OnHealthOnChanged;
                    enemy.Death -= OnEnemyOnDeath;
                    _enemiesHPPool.ReleaseObject(hpBar);
                }

                enemy.Death += OnEnemyOnDeath;
            }
        }

        public void Destroy(EnemyController enemy)
        {
            enemy.Die();
            _enemiesPool.ReleaseObject(enemy);
        }

        public void Clear()
        {
            foreach (var enemy in _enemies)
            {
                Destroy(enemy);
            }
        }
    }
}
