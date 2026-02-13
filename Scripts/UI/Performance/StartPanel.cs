
using DG.Tweening;
using MyGameSystem.Manager;
using MyUI.Dialogue;
using TMPro;
using UnityEngine;


namespace MyUI.Performance
{
    public class StartPanel : BasePanel
    {
        [SerializeField] private StartGate gate;
        [SerializeField] private TextMeshProUGUI version;
        [SerializeField] private CursorA cursorA;

        private AdvancedButtonA _continueButtonA;
        private AdvancedButtonA _restartButtonA;
        private AdvancedButtonA _quitButtonA;
        private AdvancedButtonA _infoButtonA;

        private RectTransform _rectTransform;

        protected override void Awake()
        {
            SaveManager.instance.Load();

            _rectTransform = GetComponent<RectTransform>();

            _continueButtonA = transform.Find("Options/Continue").GetComponent<AdvancedButtonA>();
            _restartButtonA = transform.Find("Options/Restart").GetComponent<AdvancedButtonA>();
            _quitButtonA = transform.Find("Options/Quit").GetComponent<AdvancedButtonA>();
            _infoButtonA = transform.Find("Options/Info").GetComponent<AdvancedButtonA>();

            _continueButtonA.InitCursorA(cursorA);
            _restartButtonA.InitCursorA(cursorA);
            _quitButtonA.InitCursorA(cursorA);
            _infoButtonA.InitCursorA(cursorA);

            version.SetText(SaveManager.instance.playerConfig.version);

            _continueButtonA.onClick.AddListener(() =>
            {
                // gate.Init(GetComponent<CanvasGroup>());
                // gate.EnterStartGate();

                UIManager.instance.OpenPanel(UIConst.MapNavigationPanel);
            });
            _restartButtonA.onClick.AddListener(() =>
            {
                PlayerPrefs.DeleteAll();
                SaveManager.instance.IsStart = false;
                SaveManager.instance.Load();

                cursorA.OnClick();

                _rectTransform.DOAnchorPosX(-600f,  1.5f).SetEase(Ease.OutSine);
                //UIManager.StopCursorA();
                UIManager.instance.ClosePanel(UIConst.StartInfoPanel);
                gate.EnterStartGate();
            });

            _quitButtonA.onClick.AddListener(() =>
            {
    #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
    #else
                Application.Quit();
    #endif
            });
            _infoButtonA.onClick.AddListener(() => UIManager.instance.OpenPanel(UIConst.StartInfoPanel));

        }

        public void Initialize()
        {

            if (!SaveManager.instance.IsStart)
            {
                Destroy(_continueButtonA.gameObject);
                _restartButtonA.GetComponentInChildren<TextMeshProUGUI>().SetText("开始游戏");
            }

            _rectTransform.DOAnchorPosX(0f,  1.5f).SetEase(Ease.OutSine);

            //
            // Debug.Log("正在showTitle");
            // Cursor.lockState = CursorLockMode.None;
            // Cursor.visible = true;
            // //startTitle.ShowTitle();
        }
    }
}
