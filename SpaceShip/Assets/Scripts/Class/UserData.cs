using System;
using System.Collections.Generic;

namespace SpaceShip
{
    [System.Serializable]
    public class UserData
    {
        public string[] levelsName;
        public string[] levelsState;
        public string[] levelsEnemyType;
        public int[] levelsAsteroidCount; 

        public UserData(IReadOnlyList<Level> _levels)
        {
            levelsName = new string[_levels.Count];
            levelsState = new string[_levels.Count];
            levelsEnemyType = new string[_levels.Count];
            levelsAsteroidCount = new int[_levels.Count]; 

            for (int i = 0; i < _levels.Count; i++)
            {
                levelsName[i] = _levels[i].Name;
                levelsState[i] = _levels[i].LevelState.ToString();
                levelsEnemyType[i] = _levels[i].EnemyType.ToString();

                levelsAsteroidCount[i] = _levels[i].AsteroidGoalCount; 
            }
        }

        public List<Level> GetLoadedUserData()
        {
            List<Level> levels = new List<Level>();

            for (int i = 0; i < levelsState.Length; i++)
            {
                Level level = new Level
                {
                    Name = levelsName[i],
                    LevelState = (LevelState) Enum.Parse(typeof(LevelState), levelsState[i]),
                    EnemyType = (EnemyType) Enum.Parse(typeof(EnemyType), levelsEnemyType[i]),
                    AsteroidGoalCount = levelsAsteroidCount[i]
                };

                levels.Add(level);
            }

            return levels;
        }
    }
}