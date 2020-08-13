using UnityEngine;

namespace SpaceShip
{
    public class AsteroidBreakEffect : MonoBehaviour
    {
        [SerializeField]
        protected ParticleSystem _particleSystem;

        public void PlayBreakEffect()
        {
            _particleSystem.Play();
        }
    }
}