using MyGameSystem.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace MyUI.Performance
{
    public class StartOptionPanel : BasePanel
    {
        private Button _closeButton;

        protected override void Awake()
        {
            base.Awake();
            _closeButton = transform.Find("TopRight/Close").GetComponent<Button>();
            _closeButton.onClick.AddListener(CloseClick);
        }

        protected override void HandleInput()
        {
            base.HandleInput();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.instance.ClosePanel(panelName);
            }
        }

        private void CloseClick()
        {
            UIManager.instance.ClosePanel(panelName);
        }
    }
}
