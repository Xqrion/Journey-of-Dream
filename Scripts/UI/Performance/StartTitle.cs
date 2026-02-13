using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyUI.Performance
{
public class StartTitle : MonoBehaviour
{
    [SerializeField] private RectTransform starLine;
    [SerializeField] private RectTransform starIcon;
    [SerializeField] private RectTransform mainTitle;
    [SerializeField] private TextMeshProUGUI[] mainTitleWords;
    [SerializeField] private RectTransform subTitle;
    [SerializeField] private TextMeshProUGUI anyButtonText;
    [SerializeField] private Image background;

    [SerializeField] private Transform cameraPosition;
    [Space(10)] [SerializeField] private StartPanel startPanel;

    private CanvasGroup _mainTitleGroup;
    private CanvasGroup _subTitleGroup;


    private bool _canEnter;

    private void Awake()
    {
        _mainTitleGroup = mainTitle.GetComponent<CanvasGroup>();
        _subTitleGroup = subTitle.GetComponent<CanvasGroup>();

        //_mainTitleWords = mainTitle.GetComponentsInChildren<RectTransform>();

        _mainTitleGroup.alpha = 0f;
        _subTitleGroup.alpha = 0f;
        anyButtonText.alpha = 0f;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);

        starLine.DOAnchorPosX(310f, 0.6f).SetEase(Ease.OutSine);
        starIcon.DOAnchorPosX(280f, 0.6f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            starIcon.DOShakeAnchorPos(5f, new Vector2(3f, 3f)).SetEase(Ease.Linear).OnComplete(() =>
            {
                starIcon.GetComponent<Image>().DOFade(0f, 0.5f).SetEase(Ease.OutSine);
                starLine.GetComponent<Image>().DOFade(0f, 0.5f).SetEase(Ease.OutSine);
            });
        });
        starLine.DOSizeDelta(new Vector2(0f, 5f), 1f).SetEase(Ease.OutSine).SetDelay(1f);


        DOVirtual.Float(0f, 1f, 3f, v =>
        {
            //_mainTitleGroup.alpha = v;
            _subTitleGroup.alpha = v;
        }).SetDelay(4f);

        _mainTitleGroup.alpha = 1f;
        //mainTitle.DOSizeDelta(new Vector2(1400f, 250f), 3f).SetEase(Ease.InOutSine).SetDelay(2f);
        foreach (var t in mainTitleWords)
        {
            t.alpha = 0f;
        }

        for (int i = 0; i < mainTitleWords.Length; i++)
        {
            mainTitleWords[i].DOFade(1f, 1.5f).SetDelay(i + 1.5f);
        }


        subTitle.DOSizeDelta(new Vector2(1400f, 150f), 3f).SetEase(Ease.InOutSine).SetDelay(4f);
        //mainTitle.DOScaleX(1.2f, 1f).SetEase(Ease.InOutQuad);
        //subTitle.DOScaleX(1.2f, 1f).SetEase(Ease.InOutQuad);

        background.DOFade(0f, 1f).SetDelay(6f).OnComplete(() => { _canEnter = true; });

        anyButtonText.DOFade(1f, 1.5f).SetLoops(-1, LoopType.Yoyo).SetDelay(6f);
        var color = new Color(1f, 0.9f, 0.6f, 1f);
        anyButtonText.DOColor(color, 1.5f).SetLoops(-1, LoopType.Yoyo).SetDelay(6f);
    }

    private void Update()
    {
        if (Input.anyKeyDown && _canEnter)
        {
            Debug.Log("Start Title 结束");
            _canEnter = false;
            EnterStartPanel();
        }
    }

    private void EnterStartPanel()
    {
        if (Camera.main != null) Camera.main.transform.DOMove(cameraPosition.position, 1.5f).SetEase(Ease.InOutSine);
        startPanel.Initialize();
        anyButtonText.gameObject.SetActive(false);
        //starIcon.DOAnchorPosX(1000f, 1.5f).SetEase(Ease.OutSine);
        DOVirtual.Float(1f, 0f, 1.5f, value =>
        {
            _mainTitleGroup.alpha = value;
            _subTitleGroup.alpha = value;
        });
        mainTitle.DOAnchorPosY(700f, 1.5f).OnComplete((() => { gameObject.SetActive(false); }));
        subTitle.DOAnchorPosY(-700f, 1.5f);
    }

}
}
