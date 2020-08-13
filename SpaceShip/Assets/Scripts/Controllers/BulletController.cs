using UnityEngine;


namespace SpaceShip
{
    public class BulletController : MonoBehaviour
    {
        public enum BulletDirection
        {
            Up = 1,
            Down = -1
        };

        [SerializeField]
        protected BulletDirection _bulletDirection;
        
        [SerializeField]
        protected float _speed;

        private void Update()
        {
            Move();
        }

        void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 15 * (int)_bulletDirection), _speed * Time.deltaTime);
        }

    }
}