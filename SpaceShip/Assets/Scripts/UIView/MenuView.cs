using UnityEngine;
using UnityEngine.UI;

namespace SpaceShip
{
    public class MenuView : UI
    {
        [SerializeField]
        protected RectTransform _rectTransform;

        [SerializeField]
        protected Button _startGameButton;
        [SerializeField]
        protected Button _exitButton;
        

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
            UIController.Instance.EventShowMenuView += ShowMenuView;
            UIController.Instance.EventHideMenuView += HideMenuView;
        }

        private void OnDisable()
        {
            UIController.Instance.EventShowMenuView -= ShowMenuView;
            UIController.Instance.EventHideMenuView -= HideMenuView;
        }

        public void ShowMenuView()
        {
            gameObject.SetActive(true);
            base.Show();
        }

        public void HideMenuView()
        {
            base.Hide();
        }

        private void AddListeners()
        {
            _startGameButton.onClick.RemoveAllListeners();
            _startGameButton.onClick.AddListener(OnStartGameButtonClick);

            _exitButton.onClick.RemoveAllListeners();
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnStartGameButtonClick()
        {
            HideMenuView();
            UIController.Instance.CallEventShowSelectLevelView();
        }

        private void OnExitButtonClick()
        {
            Application.Quit();
        }
    }
}