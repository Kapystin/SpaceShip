
using System;

namespace SpaceShip
{

    public class EventController : MasterSingleton<EventController>
    {
        public event Action<GameStatus> EventSetGameStatus;
        public event Action EventStartGame;
        public event Action<int> EventSetPlayerHP;
        public event Action EventAsteroidDestroyed;
        public event Action EventBossKilled;
        public event Action EventPlayerDied;
        public event Action EventGameOver;
        public event Action EventAsteroidCollidedWithPlayer;
        public event Action<int,int> EventSetPlayerScore;

        public void CallEventSetGameStatus(GameStatus gameStatus)
        {
            EventSetGameStatus?.Invoke(gameStatus);
        }

        public void CallEventStartGame()
        {
            EventStartGame?.Invoke();
        }

        public void CallEventSetPlayerHP(int hpValue)
        {
            EventSetPlayerHP?.Invoke(hpValue);
        } 

        public void CallEventAsteroidDestroyed()
        {
            EventAsteroidDestroyed?.Invoke();
        }

        public void CallEventAsteroidCollidedWithPlayer()
        {
            EventAsteroidCollidedWithPlayer?.Invoke();
        }

        public void CallEventSetPlayerScore(int currentValue, int goalValue)
        {
            EventSetPlayerScore?.Invoke(currentValue, goalValue);
        }

        public void CallEventBossKilled()
        {
            EventBossKilled?.Invoke();
        }

        public void CallEventPlayerDied()
        {
            EventPlayerDied?.Invoke();
        }

        public void CallEventGameOver()
        {
            EventGameOver?.Invoke();
        }
    }
}