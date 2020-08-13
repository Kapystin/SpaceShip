using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    [CreateAssetMenu(menuName = "Create Levels Library")]
    public class Levels : ScriptableObject
    {
        public const string SAVE_FILE_NAME = "SPACE_SHIP_SAVE";

        public List<Level> LevelsLibrary;
        
        public void UpdateLevelState(string tagName, LevelState levelState)
        {
            int nextLevelIndex = 0;
            for (int index = 0; index < LevelsLibrary.Count; index++)
            {
                var level = LevelsLibrary[index];

                if (level.Name == tagName)
                {
                    nextLevelIndex = index + 1; 
                    level.LevelState = levelState;
                }
            }

            if (nextLevelIndex < LevelsLibrary.Count)
            {
                if (LevelsLibrary[nextLevelIndex].LevelState != LevelState.Passed)
                {
                    LevelsLibrary[nextLevelIndex].LevelState = LevelState.Open;
                }
            }

            SaveLevels();
        }

        private List<Level> GetClearSetup()
        {
            List<Level> levels = new List<Level>();

            for (int i = 0; i < 3; i++)
            {
                Level level = new Level
                {
                    Name = $"Level {i+1}",
                    LevelState = i == 0 ? LevelState.Open : LevelState.Closed,
                    EnemyType = (EnemyType)i,
                    AsteroidGoalCount = Random.Range(3,13)
                };

                levels.Add(level);
            }

            return levels;
        }


        #region Save and Load

        public void SaveLevels()
        {
            DataController.SaveUserData(SAVE_FILE_NAME, LevelsLibrary);
        }

        public void LoadLevels()
        {
            UserData userData = DataController.LoadUserData(SAVE_FILE_NAME);
            if (userData is null)
            {
                Debug.Log($"UserData is null = {userData is null}");
                LevelsLibrary = GetClearSetup();
                SaveLevels();
            }
            else
            {
                Debug.Log($"UserData is null = {userData is null}");
                LevelsLibrary = userData.GetLoadedUserData();
            }
        }

        #endregion
    }
}