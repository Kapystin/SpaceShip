using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceShip
{
    public class LevelItem : MonoBehaviour
    {
        [SerializeField]
        protected Button _levelButton;
        [SerializeField]
        protected TMP_Text _levelName;

        private Level _level;

        public void UpdateUI(Level level)
        {
            _levelName.text = $"{level.Name}\n{level.LevelState}";
            _level = level;

            _levelButton.interactable = level.LevelState != LevelState.Closed;
            AddListeners();
        }

        private void AddListeners()
        {
            _levelButton.onClick.RemoveAllListeners();
            _levelButton.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        { 
            GameState.LevelConfig = new LevelConfig(_level);
            SceneManager.LoadScene(1);
        }
    }
}