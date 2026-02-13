using DG.Tweening;
using MyGameSystem.Manager;
using MyGameSystem.Scene;
using MyUI.Dialogue;
using UnityEngine;

namespace MyUI.Player
{
    public class PausePanel : BasePanel
    {
        [SerializeField] private CursorA cursorA;
        [SerializeField] private AdvancedButtonA resumeButton;
        [SerializeField] private AdvancedButtonA optionButton;
        [SerializeField] private AdvancedButtonA mainMenuButton;

        private CanvasGroup canvasGroup;

        private Tween _tween;
        //private bool _previousInput;

        protected override void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            // ressumeButton = transform.Find("Content/ButtonResume").GetComponent<AdvancedButtonA>();
            // optionButton = transform.Find("Content/ButtonOptions").GetComponent<AdvancedButtonA>();
            // mainMenuButton = transform.Find("Content/ButtonMainMenu").GetComponent<AdvancedButtonA>();

            resumeButton.InitCursorA(cursorA);
            optionButton.InitCursorA(cursorA);
            mainMenuButton.InitCursorA(cursorA);

            optionButton.onClick.AddListener(() =>
            {
                SceneLoader.instance.LoadScene(GameManager.instance.CurrentScene ,"重启当前场景 i n g");
            });
            resumeButton.onClick.AddListener(() =>
            {
                UIManager.instance.ClosePanel(UIConst.PausePanel);
            });
            mainMenuButton.onClick.AddListener(() =>
            {
                UIManager.instance.ClosePanel(UIConst.PausePanel);
                UIManager.instance.ClosePanel(UIConst.BarPanel);
                SceneLoader.instance.LoadScene(SceneType.Start);
            });

        }
        public override void OnPanelOpen(string openPanelName)
        {
            enableInput = false;
            base.OnPanelOpen(openPanelName);

            GameManager.instance.AblePlayerInput(false);
            GameManager.instance.playerController.UpdateCursorLock(false);

            //Time.timeScale = 0f;
        }

        protected override void OpenAnimation()
        {
            base.OpenAnimation();
            _tween = DOVirtual.Float(0, 1f, 0.2f, v =>
            {
                canvasGroup.alpha = v;
            }).OnComplete(() => enableInput = true);
        }

        public override void OnPanelClose()
        {
            base.OnPanelClose();

            GameManager.instance.AblePlayerInput(true);
            GameManager.instance.playerController.UpdateCursorLock(true);

            //Time.timeScale = 1f;
        }

        protected override void HandleInput()
        {
            base.HandleInput();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _tween.Kill();
                UIManager.instance.ClosePanel(panelName);
            }
        }
    }
}
