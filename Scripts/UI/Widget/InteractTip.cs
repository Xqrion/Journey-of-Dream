using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyUI.Widget
{
    public class InteractTip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI interactTipText;
        //[SerializeField] private bool showIcon;
        [SerializeField] private GameObject icon;
        [SerializeField] private TextMeshProUGUI iconText;

        [SerializeField] private Image maskImage;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void ShowInteractTip(string content, bool showIcon = true, string iconText = "F")
        {
            gameObject.SetActive(true);
            icon.SetActive(showIcon);
            this.iconText.SetText(iconText);
            interactTipText.SetText(content);
            maskImage.fillAmount = 0f;

            DOVirtual.Float(0f, 1f, 0.4f, (v =>
            {
                maskImage.fillAmount = v;
            }));
        }

        public void SustainInteractTip()
        {
            gameObject.SetActive(true);
            maskImage.fillAmount = 1f;
        }

        public void HideInteractTip()
        {
            DOVirtual.Float(1f, 0f, 0.4f, (v =>
            {
                maskImage.fillAmount = v;
            })).OnComplete((() =>
            {
                gameObject.SetActive(false);
            }));

        }






    }
}
