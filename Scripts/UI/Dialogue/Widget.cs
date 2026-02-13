using System;
using System.Collections;
using UnityEngine;

namespace MyUI.Dialogue
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Widget : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private readonly AnimationCurve _fadingCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        public float RenderOpacity { get => canvasGroup.alpha; set => canvasGroup.alpha = value; }

        [Tooltip("Only for AdvancedText!")] public float intervalTime = 0.2f;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private Coroutine _fadeCoroutine;
        //private Tweener _fadeTween;

        public void Fade(float opacity, float duration, Action onFinished)
        {
            if (duration <= 0)
            {
                RenderOpacity = opacity;
                onFinished?.Invoke();
            }
            else
            {
                // if (_fadeTween.IsPlaying())
                // {
                //     _fadeTween.Kill();
                // }
                //_fadeCoroutine =

                if (_fadeCoroutine != null)
                {
                    StopCoroutine(_fadeCoroutine);
                }
                _fadeCoroutine = StartCoroutine(Fading(opacity, duration, onFinished));
            }
        }

        private IEnumerator Fading(float opacity, float duration, Action onFinished)
        {
            float timer = 0;
            float start = RenderOpacity;
            while (timer < duration)
            {
                timer = Mathf.Min(duration, timer + Time.unscaledDeltaTime);
                RenderOpacity = Mathf.Lerp(start, opacity, _fadingCurve.Evaluate(timer / duration));
                yield return null;
            }

            onFinished?.Invoke();
        }
    }
}
