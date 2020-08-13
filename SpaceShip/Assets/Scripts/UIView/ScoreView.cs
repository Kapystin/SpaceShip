using TMPro;
using UnityEngine;

namespace SpaceShip
{
    public class ScoreView : UI
    {

        [SerializeField]
        protected TMP_Text _asteroidScoreHolder;

        private void Start()
        {
            if (!_isVisibleAtStart)
            {
                Hide(0);
            }
        }

        private void OnEnable()
        {
            UIController.Instance.EventShowScoreView += ShowScoreView;
            UIController.Instance.EventHideScoreView += HideScoreView;

            EventController.Instance.EventSetPlayerScore += SetAsteroidScore;
        }

        private void OnDisable()
        {
            UIController.Instance.EventShowScoreView -= ShowScoreView;
            UIController.Instance.EventHideScoreView -= HideScoreView;

            EventController.Instance.EventSetPlayerScore -= SetAsteroidScore;
        }

        public void ShowScoreView()
        {
            gameObject.SetActive(true);
            base.Show();
        }

        public void HideScoreView()
        {
            base.Hide();
        }

        private void SetAsteroidScore(int currentValue, int goalValue)
        {
            _asteroidScoreHolder.text = $"{currentValue} / {goalValue}";
        }
    }
}