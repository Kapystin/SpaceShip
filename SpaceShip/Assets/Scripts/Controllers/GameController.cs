using TMPro;
using UnityEngine;

namespace SpaceShip
{
    public class GameController : MasterSingleton<GameController>
    {
        [SerializeField]
        protected GameStatus _gameStatus;
        [SerializeField]
        protected float _spawnAsteroidRate = 1f;
        [SerializeField]
        protected TMP_Text _levelNameTextHolder;

        private int _currentAsteroidScore;
        private int _asteroidGoalCount;
        private float _nextAsteroidSpawnTime;
        private bool _bossSpawned;
        private Level _selectedLevel;

        private void Start()
        {
            _selectedLevel = GameState.LevelConfig.SelectedLevelSettings;
            _asteroidGoalCount = _selectedLevel.AsteroidGoalCount;
            _levelNameTextHolder.text = $"{_selectedLevel.Name}";
            EventController.Instance.CallEventSetPlayerScore(_currentAsteroidScore, _asteroidGoalCount);
        }

        private void OnEnable()
        {
            EventController.Instance.EventStartGame += OnGameStartEvent;
            EventController.Instance.EventAsteroidDestroyed += AddScore;
            EventController.Instance.EventBossKilled += OnBossKilled;
            EventController.Instance.EventPlayerDied += OnPlayerDied;
        }

        private void OnDisable()
        {
            EventController.Instance.EventStartGame -= OnGameStartEvent;
            EventController.Instance.EventAsteroidDestroyed -= AddScore;
            EventController.Instance.EventBossKilled -= OnBossKilled;
            EventController.Instance.EventPlayerDied -= OnPlayerDied;
        }

        private void Update()
        {
            if (_nextAsteroidSpawnTime < 0)
            {
                SpawnAsteroid();
                _nextAsteroidSpawnTime = _spawnAsteroidRate;
            }
            _nextAsteroidSpawnTime -= Time.deltaTime;
        }

        private void SpawnAsteroid()
        {
            if (_gameStatus == GameStatus.Play) 
            {
                float random = Random.Range(-3, 3);
                GameObject asteroid = ObjectPoolController.Instance.SpawnFromPool("Asteroid", new Vector3(random, 11, 0), Quaternion.identity);
                asteroid.GetComponent<AsteroidController>().SetTargetPosition(random);
            }
        }

        private void OnGameStartEvent()
        {
            UIController.Instance.CallEventShowHealthView();
            UIController.Instance.CallEventShowScoreView();

            _gameStatus = GameStatus.Play;

            ObjectPoolController.Instance.SpawnFromPool("Player", new Vector3(0, -3, 0), Quaternion.identity);
        }

        private void AddScore()
        {
            _currentAsteroidScore++;
            if (_currentAsteroidScore >= _asteroidGoalCount)
            {
                _currentAsteroidScore = _asteroidGoalCount; 
                if (!_bossSpawned)
                {
                    SpawnBoss();
                    UIController.Instance.CallEventHideScoreView();
                    _bossSpawned = true;
                }

            }
            EventController.Instance.CallEventSetPlayerScore(_currentAsteroidScore, _asteroidGoalCount);
        }

        private void SpawnBoss()
        {
            ObjectPoolController.Instance.SpawnFromPool("Enemy", new Vector3(0, 11, 0), Quaternion.identity);
        }

        private void OnBossKilled()
        {
            GameOver();
            if (Settings.SoundLibrary.GetAudioClip("WinGame") != null)
            {
                SoundController.Instance.PlayAudioClip("WinGame");
            }
            UIController.Instance.CallEventShowGameOverView("GAME OVER \n YOU WIN! ");
            Settings.Levels.UpdateLevelState(_selectedLevel.Name, LevelState.Passed);
        }
        
        private void OnPlayerDied()
        {
            GameOver();
            if (Settings.SoundLibrary.GetAudioClip("LoseGame") != null)
            {
                SoundController.Instance.PlayAudioClip("LoseGame");
            }
            UIController.Instance.CallEventShowGameOverView("GAME OVER \n YOU LOSE! ");
        }

        public void GameOver()
        {
            _gameStatus = GameStatus.GameOver;
            EventController.Instance.CallEventGameOver();
            UIController.Instance.CallEventHideHealthView();
            UIController.Instance.CallEventHideScoreView();

        }
    }
}