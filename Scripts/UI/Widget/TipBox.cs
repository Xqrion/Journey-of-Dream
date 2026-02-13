using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MyUI.Widget
{
    public class TipBox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private RectTransform rectTransform;

        private void Awake()
        {
            gameObject.SetActive(false);
            rectTransform.anchoredPosition = new Vector3(0f, -200f, 0f);
        }
        public void ShowGameTip(string content)
        {
            gameObject.SetActive(true);
            gameObject.SetActive(true);
            rectTransform.anchoredPosition = new Vector3(0, -200, 0);
            rectTransform.DOAnchorPosX(440f, 1f).SetEase(Ease.OutSine);
            text.SetText(content);
            rectTransform.DOAnchorPosX(0f, 0.5f).SetEase(Ease.InSine).SetDelay(4f).OnComplete(() => gameObject.SetActive(false));

        }



    }
}
