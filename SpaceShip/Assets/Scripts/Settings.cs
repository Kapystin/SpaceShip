using UnityEngine;

namespace SpaceShip
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] protected Levels _levels;
        [SerializeField] protected SoundLibrary _soundLibrary;

        public static Levels Levels;
        public static SoundLibrary SoundLibrary;

        private void Awake()
        {
            if (Levels == null)
            {
                Levels = _levels;
            }

            if (SoundLibrary == null)
            {
                SoundLibrary = _soundLibrary;
            }
        }

    }
}