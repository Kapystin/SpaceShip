using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        protected BoxCollider2D _boxCollider2D;
        [SerializeField]
        protected SpriteRenderer _spriteRenderer;
        [SerializeField]
        protected float _yOffsetFromPlayer;
        [SerializeField]
        protected int _health;
        [SerializeField]
        protected float _speed;
        [SerializeField]
        protected SpriteRenderer _enemySprite;
        [SerializeField]
        protected List<Sprite> _enemySprites;
        [SerializeField]
        [Range(0.5f, 5f)]
        protected float _spawnBulletRate = 1f;
        private float _nextBulletSpawnTime;
        private GameObject _target;

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player");
            _spriteRenderer.color = Color.white;
            switch (GameState.LevelConfig.SelectedLevelSettings.EnemyType)
            {
                case EnemyType.Red:
                    _enemySprite.sprite = _enemySprites[0];
                    break;
                case EnemyType.Blue:
                    _enemySprite.sprite = _enemySprites[1];
                    break;
                case EnemyType.Green:
                    _enemySprite.sprite = _enemySprites[2];
                    break;
                default:
                    break;
            }

        }

        private void OnEnable()
        {
            EventController.Instance.EventGameOver += OnGameOver;
        }

        private void OnDisable()
        {
            EventController.Instance.EventGameOver -= OnGameOver;
        }

        private void Update()
        {
            MoveToTarget();

            if (_nextBulletSpawnTime < 0)
            {
                Shot();
                _nextBulletSpawnTime = _spawnBulletRate;
            }
            _nextBulletSpawnTime -= Time.deltaTime;
        }

        private void MoveToTarget()
        {
            if (transform.position.y > _yOffsetFromPlayer + 1)
            {
                _boxCollider2D.enabled = false;
            }
            else
            {
                _boxCollider2D.enabled = true;
            }
            Vector2 targetPosition = new Vector2(_target.transform.position.x, _target.transform.position.y + _yOffsetFromPlayer);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        }

        private void Shot()
        {
            if (Vector2.Distance(_target.transform.position, transform.position) <= (_yOffsetFromPlayer + (gameObject.transform.localScale.x / 2)))
            {
                ObjectPoolController.Instance.SpawnFromPool("EnemyBullet", transform.position, Quaternion.identity);
                if (Settings.SoundLibrary.GetAudioClip("EnemyShot") != null)
                {
                    SoundController.Instance.PlayAudioClip("EnemyShot");
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayerBullet"))
            { 
                GetDamage(1);
                collision.gameObject.SetActive(false);
            }
        }

        private void GetDamage(int damage)
        {
            if (Settings.SoundLibrary.GetAudioClip("EnemyHit") != null)
            {
                SoundController.Instance.PlayAudioClip("EnemyHit");
            }

            _health -= damage;
            StartCoroutine(FlashEffect());
            if (_health <= 0)
            {
                _health = 0;
                EventController.Instance.CallEventBossKilled();
                gameObject.SetActive(false);
            }
        }

        private void OnGameOver()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator FlashEffect()
        {
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            if (!gameObject.activeSelf)
            {
                yield break;
            }
            _spriteRenderer.color = Color.white;
        }
    }
}
