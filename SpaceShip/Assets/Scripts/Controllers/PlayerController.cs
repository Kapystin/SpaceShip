using System.Collections;
using UnityEngine;

namespace SpaceShip
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        protected Rigidbody2D _rigidbody2D;
        [SerializeField]
        protected SpriteRenderer _spriteRenderer;
        [SerializeField]
        protected int _health;
        [SerializeField]
        protected float _playerMoveSpeed; 
        [SerializeField]
        protected VariableJoystick _variableJoystick;
        [SerializeField]
        protected Vector2 _borderLimit;
        [SerializeField]
        [Range(0.5f, 5f)]
        protected float fireRate = 1f;
        
        private float fireCoolDown;
        private Vector2 _movement = Vector2.zero;

        private void Start()
        { 
            EventController.Instance.CallEventSetPlayerHP(_health);
            _variableJoystick = FindObjectOfType<VariableJoystick>();
            _spriteRenderer.color = Color.white;
        }

        private void OnEnable()
        {
            EventController.Instance.EventAsteroidCollidedWithPlayer += OnCollideWithAsteroid;
            EventController.Instance.EventGameOver += OnGameOver;
        }

        private void OnDisable()
        {
            EventController.Instance.EventAsteroidCollidedWithPlayer -= OnCollideWithAsteroid;
            EventController.Instance.EventGameOver -= OnGameOver;
        }

        private void Update()
        {
            if (Application.isEditor)
            {
                _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
            else
            {
                _movement = Vector2.up * _variableJoystick.Vertical + Vector2.right * _variableJoystick.Horizontal;
            }


            if (fireCoolDown < 0f)
            { 
                Shot();
                fireCoolDown = 1f / fireRate;
            }
            fireCoolDown -= Time.deltaTime;

        }

        private void FixedUpdate()
        {
            Move(); 
        }

        private void Move()
        {
            _rigidbody2D.MovePosition((Vector2)transform.position + (_movement * _playerMoveSpeed * Time.fixedDeltaTime));

            if (_rigidbody2D.position.x > _borderLimit.x)
            { 
                _rigidbody2D.position = Vector2.Lerp(_rigidbody2D.position, new Vector2(_borderLimit.x, _rigidbody2D.position.y), 0.7f);
            }
            if (_rigidbody2D.position.x < -_borderLimit.x)
            {
                _rigidbody2D.position = Vector2.Lerp(_rigidbody2D.position, new Vector2(-_borderLimit.x, _rigidbody2D.position.y), 0.7f);
            } 
            
            if (_rigidbody2D.position.y > _borderLimit.y)
            { 
                _rigidbody2D.position = Vector2.Lerp(_rigidbody2D.position, new Vector2(_rigidbody2D.position.x, _borderLimit.y), 0.7f);
            }
            if (_rigidbody2D.position.y < -_borderLimit.y)
            {
                _rigidbody2D.position = Vector2.Lerp(_rigidbody2D.position, new Vector2(_rigidbody2D.position.x , - _borderLimit.y), 0.7f);
            }
        }

        private void Shot()
        {
            ObjectPoolController.Instance.SpawnFromPool("PlayerBullet", transform.position, Quaternion.identity);
            if (Settings.SoundLibrary.GetAudioClip("PlayerShot") != null)
            {
                SoundController.Instance.PlayAudioClip("PlayerShot", 0.3f);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("EnemyBullet"))
            {
                GetDamage(1);
                collision.gameObject.SetActive(false);
            }
        }

        private void OnCollideWithAsteroid()
        {
            GetDamage(1);
        }

        private void GetDamage(int damage)
        {
            if (Settings.SoundLibrary.GetAudioClip("PlayerHit") != null)
            {
                SoundController.Instance.PlayAudioClip("PlayerHit");
            }

            _health -= damage;
            StartCoroutine(FlashEffect());
            if (_health <= 0)
            {
                _health = 0;
                EventController.Instance.CallEventPlayerDied();
                gameObject.SetActive(false);
            }

            EventController.Instance.CallEventSetPlayerHP(_health);
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