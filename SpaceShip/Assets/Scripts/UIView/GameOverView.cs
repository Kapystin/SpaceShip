using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceShip
{
    public class GameOverView : UI
    {
        [SerializeField]
        protected RectTransform _rectTransform;
        [SerializeField]
        protected Button _backToMenuButton;
        [SerializeField]
        protected TMP_Text _labelTextHolder;

        
        private void Start()
        {
            if (!_isVisibleAtStart)
            {
                Hide(0);
            }

            SetZeroPosition(_rectTransform);
            AddListeners();
        }

        private void OnEnable()
        {
            UIController.Instance.EventShowGameOverView += ShowGameOverView;
            UIController.Instance.EventHideGameOverView += HideGameOverView;
        }

        private void OnDisable()
        {
            UIController.Instance.EventShowGameOverView -= ShowGameOverView;
            UIController.Instance.EventHideGameOverView -= HideGameOverView;
        }

        public void ShowGameOverView(string labelText)
        {
            _labelTextHolder.text = labelText;
            gameObject.SetActive(true);
            base.Show();
        }

        public void HideGameOverView()
        {
            base.Hide();
        }

        private void AddListeners()
        {
            _backToMenuButton.onClick.RemoveAllListeners();
            _backToMenuButton.onClick.AddListener(OnBackToMenuButtonClick);
        }

        private void OnBackToMenuButtonClick()
        {
            SceneManager.LoadScene(0);
        }
    }
}