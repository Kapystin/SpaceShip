using UnityEngine;

namespace SpaceShip
{
    public class LoadController : MonoBehaviour
    {
        private void Awake()
        {
            Settings.Levels.LoadLevels();
        }
    }
}