using UnityEngine;
using UnityEngine.UI;

namespace SpaceShip
{
    public class SelectLevelView : UI
    {
        [SerializeField]
        protected CanvasGroup _canvasGroup;
        [SerializeField]
        protected RectTransform _rectTransform;
        [SerializeField]
        protected Transform _levelsHolder;

        [SerializeField]
        protected Levels _levels;
        [SerializeField]
        protected LevelItem _levelItem;

        [SerializeField]
        protected Button _backButton;
          
        private void Awake()
        {
            base._canvasGroupParent = _canvasGroup;
        }

        private void Start()
        {
            if (!_isVisibleAtStart)
            {
                Hide(0);
            }

            AddListeners();
            SetZeroPosition(_rectTransform);
        }

        private void OnEnable()
        {
            UIController.Instance.EventShowSelectLevelView += ShowSelectLevelView;
            UIController.Instance.EventHideSelectLevelView += HideSelectLevelView;
        }

        private void OnDisable()
        {
            UIController.Instance.EventShowSelectLevelView -= ShowSelectLevelView;
            UIController.Instance.EventHideSelectLevelView -= HideSelectLevelView;
        }

        public void ShowSelectLevelView()
        {
            FillLevelsButton();
            base.Show();
        }

        public void HideSelectLevelView()
        {
            base.Hide();
        }

        private void AddListeners()
        {
            _backButton.onClick.RemoveAllListeners();
            _backButton.onClick.AddListener(OnBackButtonClick);
        }

        private void OnBackButtonClick()
        {
            HideSelectLevelView();
            UIController.Instance.CallEventShowMenuView();
        }

        private void FillLevelsButton()
        {
            foreach (Transform child in _levelsHolder)
            {
                GameObject.Destroy(child.gameObject);
            }

            foreach (var level in _levels.LevelsLibrary)
            {
                var levelItem = Instantiate(_levelItem, _levelsHolder);
                levelItem.UpdateUI(level);
            }

        }
    }
}