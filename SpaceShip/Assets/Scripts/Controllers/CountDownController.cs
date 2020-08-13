using System.Collections;
using TMPro;
using UnityEngine;

namespace SpaceShip
{
    public class CountDownController : MonoBehaviour
    {
        [SerializeField]
        protected int _countDownTime;
        [SerializeField]
        protected TMP_Text _countDownTextHolder;

        private void Start()
        {
            StartCoroutine(StartCountDown());
        }

        private IEnumerator StartCountDown()
        {
            while (_countDownTime > 0)
            {
                _countDownTextHolder.text = $"{_countDownTime}";

                yield return new WaitForSeconds(1f);

                _countDownTime--;
            }

            _countDownTextHolder.text = $"START!";

            yield return new WaitForSeconds(1f);

            EventController.Instance.CallEventStartGame();
            _countDownTextHolder.gameObject.SetActive(false);
        }
    }
}