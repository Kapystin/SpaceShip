using System.Collections;
using UnityEngine;

namespace SpaceShip
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(RectTransform))]
    public class UI : MonoBehaviour
    {
        private const float DELAY_TIME = 0.03f;

        [SerializeField] 
        protected bool _isVisibleAtStart;
        [SerializeField]
        protected CanvasGroup _canvasGroupParent;
        private float _changeStepValue = 0.1f;
 
        public virtual void Show(float delayTime = DELAY_TIME)
        {
            StartCoroutine(SetAlphaValue(0f, 1f, delayTime));
        }

        public virtual void Hide(float delayTime = DELAY_TIME)
        {
            StartCoroutine(SetAlphaValue(1f, 0f, delayTime));
        }

        private IEnumerator SetAlphaValue(float startValue, float endValue, float delay)
        {
            if (delay == 0)
            {
                _canvasGroupParent.alpha = endValue;
            }

            if (startValue == endValue)
            {
                yield break;
            }

            if (startValue > endValue)
            {
                _changeStepValue *= -1f;
            }
            else
            {
                if (_changeStepValue < 0)
                {
                    _changeStepValue *= -1f;
                }
            }

            while (_canvasGroupParent.alpha != endValue)
            {
                _canvasGroupParent.alpha += _changeStepValue;
                yield return new WaitForSeconds(delay);
            }

            _canvasGroupParent.interactable = _canvasGroupParent.alpha != 0;
            _canvasGroupParent.blocksRaycasts = _canvasGroupParent.alpha != 0;
        }

        public virtual void SetZeroPosition(RectTransform rectTransform)
        {
            rectTransform.offsetMax = new Vector2(0,0);
            rectTransform.offsetMin = new Vector2(0,0);
        }
    }
}
