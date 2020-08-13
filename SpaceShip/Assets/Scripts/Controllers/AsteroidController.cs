using System.Collections; 
using UnityEngine;

namespace SpaceShip
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class AsteroidController : MonoBehaviour
    {
        [SerializeField]
        protected CircleCollider2D _circleCollider2D;
        [SerializeField]
        protected SpriteRenderer _spriteRenderer; 
        [SerializeField]
        protected int _health = 3;
        [SerializeField]
        protected float _speed = 2.5f;

        private Vector2 _targetPosition = Vector2.zero;

        private void Update()
        {
            Move();
        }

        public void SetTargetPosition(float _randomNumber)
        {
            _spriteRenderer.color = Color.white;
            _targetPosition = new Vector2(_randomNumber, -11);
        }

        private void Move()
        {
            if (transform.position.y > 7f)
            {
                _circleCollider2D.enabled = false;
            }
            else
            {
                _circleCollider2D.enabled = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                EventController.Instance.CallEventAsteroidCollidedWithPlayer();
                EventController.Instance.CallEventAsteroidDestroyed();
                gameObject.SetActive(false);
            }

            if (collision.CompareTag("PlayerBullet"))
            { 
                GetDamage(1);
                collision.gameObject.SetActive(false);
            }
        }

        private void GetDamage(int damage)
        {
            if (Settings.SoundLibrary.GetAudioClip("AsteroidHit") != null)
            {
                SoundController.Instance.PlayAudioClip("AsteroidHit");
            }

            _health -= damage;
            StartCoroutine(FlashEffect());

            if (_health <= 0)
            {
                EventController.Instance.CallEventAsteroidDestroyed();
                var breakEffect = ObjectPoolController.Instance.SpawnFromPool("AsteroidBreakEffect", transform.position, Quaternion.identity);
                breakEffect.GetComponent<AsteroidBreakEffect>().PlayBreakEffect();
                
                if (Settings.SoundLibrary.GetAudioClip("BreakAsteroid") != null)
                {
                    SoundController.Instance.PlayAudioClip("BreakAsteroid");
                }
                gameObject.SetActive(false);
            }
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