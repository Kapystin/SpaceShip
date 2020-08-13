using UnityEngine;

namespace SpaceShip
{
    public class HealthView : UI
    {
        [SerializeField]
        protected GameObject _heartPrefab;

        private void Start()
        {
            if (!_isVisibleAtStart)
            {
                Hide(0);
            }
        }

        private void OnEnable()
        {
            UIController.Instance.EventShowHealthView += ShowHealthView;
            UIController.Instance.EventHideHealthView += HideHealthView;

            EventController.Instance.EventSetPlayerHP += SetPlayerHP;
        }

        private void OnDisable()
        {
            UIController.Instance.EventShowHealthView -= ShowHealthView;
            UIController.Instance.EventHideHealthView -= HideHealthView;

            EventController.Instance.EventSetPlayerHP -= SetPlayerHP;
        }

        public void ShowHealthView()
        {
            gameObject.SetActive(true);
            base.Show();
        }

        public void HideHealthView()
        {
            base.Hide();
        }

        public void SetPlayerHP(int hpValue)
        {
            foreach (Transform child in gameObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            if (hpValue == 0)
            {
                return;
            }    

            for (int i = 0; i < hpValue; i++)
            {
                GameObject healthObj = Instantiate(_heartPrefab);
                healthObj.transform.SetParent(transform);
                healthObj.transform.localScale = new Vector3(1, 1, 1);
                healthObj.transform.localPosition = new Vector3((i * 100), 0, 0);
            }
        }

    }
}