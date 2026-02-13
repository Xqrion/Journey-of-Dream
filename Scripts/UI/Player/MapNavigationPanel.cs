using DG.Tweening;
using MyGameSystem.Manager;
using MyGameSystem.Scene;
using MyUI.SceneMap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyUI.Player
{
    public class MapNavigationPanel : BasePanel
    {
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        [SerializeField] private RectTransform scrollCenter;
        [SerializeField] private RectTransform foreground;

        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI subtitleText;
        [Space]
        private SceneCellSequence _sceneCellSequence;
        [SerializeField] private int index;
        [SerializeField] private int scrollLength;
        [SerializeField] private GameObject sceneElementPrefab;

        //private bool enableInput;
        //private bool _previousInput;


        protected override void Awake()
        {
            base.Awake();

            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();

            _sceneCellSequence = UIManager.instance.GetSceneCellSequence();

            scrollCenter.anchoredPosition = new Vector2(0f, 0f);

        }

        protected override void HandleInput()
        {
            base.HandleInput();

            if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.instance.ClosePanel(panelName);
            }

            if (enableInput && Input.GetKeyDown(KeyCode.Return))
            {
                EnterScene();
            }
        }

        protected override void Start()
        {
            _sceneCellSequence.Refresh();
            Refresh();
            leftButton.onClick.AddListener(OnClickLeftButton);
            rightButton.onClick.AddListener(OnClickRightButton);

            if(scrollLength > 0)
            {
                titleText.SetText(_sceneCellSequence.sceneSequence[index].sceneTitle);
                subtitleText.SetText(_sceneCellSequence.sceneSequence[index].sceneSubtitle);
            }

            if (index == 0) leftButton.interactable = false;
            if (index == scrollLength - 1 || scrollLength == 0) rightButton.interactable = false;
        }

        private void Refresh()
        {
            enableInput = true;

            foreach (var t in _sceneCellSequence.sceneSequence)
            {
                if (!t.isFinished) continue;

                scrollLength++;
                var sceneCell = Instantiate(sceneElementPrefab.transform, scrollCenter);
                var sceneImage = Resources.Load<Sprite>("Art/ScenePicture/"
                                                        + t.imageName);
                sceneCell.Find("Image").GetComponent<Image>().sprite =  sceneImage;
            }
        }

        public override void OnPanelOpen(string openPanelName)
        {
            base.OnPanelOpen(openPanelName);

            enableInput = false;

            GameManager.instance.AblePlayerInput(false);
            GameManager.instance.AblePlayerCursor(true);



        }

        protected override void OpenAnimation()
        {
            base.OpenAnimation();
            DOVirtual.Float(0f, 1f, 0.5f, v =>
            {
                _canvasGroup.alpha = v;
            });
            _rectTransform.anchoredPosition = new Vector2(0, -900f);
            _rectTransform.DOAnchorPosY(0f, 0.5f).SetEase(Ease.OutSine).OnComplete(() => enableInput = true);
        }

        public override void OnPanelClose()
        {
            enableInput = false;
            if (GameManager.instance.CurrentScene != SceneType.Start)
            {
                GameManager.instance.AblePlayerInput(true);
                GameManager.instance.playerController.UpdateCursorLock(true);
            }
            DOVirtual.Float(1f, 0f, 0.5f, v =>
            {
                _canvasGroup.alpha = v;
            }).onComplete += () =>
            {
                base.OnPanelClose();
            };
        }

        private void OnClickLeftButton()
        {
            if (index == 0)
            {
                Debug.LogError("场景索引有问题");
                return;
            }

            titleText.SetText("");
            subtitleText.SetText("");
            leftButton.interactable = false;
            index--;
            enableInput = false;

            //DOTween Animation
            scrollCenter.DOAnchorPosX(scrollCenter.anchoredPosition.x + 700f, 0.5f)
                .SetEase(Ease.OutSine).OnComplete(() =>
                {
                    leftButton.interactable = true;
                    titleText.SetText(_sceneCellSequence.sceneSequence[index].sceneTitle);
                    subtitleText.SetText(_sceneCellSequence.sceneSequence[index].sceneSubtitle);

                    if (index == 0) leftButton.interactable = false;
                    rightButton.interactable = true;
                    enableInput = true;
                });


        }

        private void OnClickRightButton()
        {
            if (index == scrollLength - 1)
            {
                Debug.LogError("场景索引有问题");
                return;
            }

            titleText.SetText("");
            subtitleText.SetText("");
            rightButton.interactable = false;
            index++;
            enableInput = false;

            //DOTween Animation
            scrollCenter.DOAnchorPosX(scrollCenter.anchoredPosition.x - 700f, 0.5f)
                .SetEase(Ease.OutSine).OnComplete(() =>
                {
                    rightButton.interactable = true;
                    //leftButton.
                    titleText.SetText(_sceneCellSequence.sceneSequence[index].sceneTitle);
                    subtitleText.SetText(_sceneCellSequence.sceneSequence[index].sceneSubtitle);

                    if (index == scrollLength - 1) rightButton.interactable = false;
                    leftButton.interactable = true;
                    enableInput = true;

                });
        }

        private void EnterScene()
        {

            foreground.DOScaleX(1f, 1f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                if (GameManager.instance.CurrentScene == _sceneCellSequence.sceneSequence[index].sceneType)
                {
                    UIManager.instance.ClosePanel(UIConst.MapNavigationPanel);
                }
                else
                    SceneLoader.instance.LoadScene(_sceneCellSequence.sceneSequence[index].sceneType);
            });


        }

    }
}
