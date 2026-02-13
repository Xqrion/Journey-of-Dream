using DG.Tweening;
using MyGameSystem.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace MyUI.Performance
{
    public class StartInfoPanel : BasePanel
    {
        private Button _closeButton;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        protected override void Awake()
        {
            base.Awake();
            _closeButton = GetComponentInChildren<Button>();
            _closeButton.onClick.AddListener(CloseClick);
            _rectTransform = GetComponent<RectTransform>();

            _canvasGroup = GetComponent<CanvasGroup>();

            _canvasGroup.alpha = 0f;
        }

        protected override void HandleInput()
        {
            base.HandleInput();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.instance.ClosePanel(panelName);
            }
        }


        protected override void OpenAnimation()
        {
            base.OpenAnimation();
            _rectTransform.DOAnchorPosX(100f, 0.5f).SetEase(Ease.OutSine).From();
            _canvasGroup.alpha = 1f;
        }

        private void CloseClick()
        {
            UIManager.instance.ClosePanel(panelName);
        }
    }
}
