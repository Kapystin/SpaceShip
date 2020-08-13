using UnityEngine;

namespace SpaceShip
{
    public class LoadDebugElements : MonoBehaviour
    {
        [SerializeField]
        protected bool _useDebug;

        [SerializeField]
        protected Level debugLevelSetting; 

        private void Awake()
        { 
            if (!Application.isEditor)
            {
                return;
            }

            if (!_useDebug)
            {
                return;
            }

            if (GameState.LevelConfig == null)
            {
                GameState.LevelConfig = new LevelConfig(debugLevelSetting); 
            }
        }
    }
}