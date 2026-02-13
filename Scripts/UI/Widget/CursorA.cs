
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CursorA : MonoBehaviour
{
    private Image _cursorAImage;
    private CanvasGroup _canvasGroup;
    [SerializeField] private float moveX = -10f;
    [SerializeField] private float moveY = -10f;
    [Space(10)] [SerializeField] private float followTime = 0.4f;

    private RectTransform _imageRectTransform;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.localScale = Vector3.one;

        _canvasGroup =  GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
        _cursorAImage = GetComponentInChildren<Image>();
        _imageRectTransform = _cursorAImage.GetComponent<RectTransform>();
    }

    // private void OnEnable()
    // {
    //     _imageRectTransform.DOAnchorPos(new Vector2(moveX, moveY), 0.5f).SetEase(Ease.InSine).SetLoops(-1, LoopType.Yoyo);
    //     DOVirtual.Float(0f, 1f, 0.5f, v => _canvasGroup.alpha = v);
    // }

    public void OpenCursor()
    {
        gameObject.SetActive(true);
        _rectTransform.localScale = Vector3.one;
        DOVirtual.Float(0f, 1f, 0.5f, v => _canvasGroup.alpha = v);
        _imageRectTransform.DOAnchorPos(new Vector2(moveX, moveY), 0.5f).SetEase(Ease.InSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void OnClick()
    {
        DOVirtual.Float(1f, 0f, 0.5f, v => _canvasGroup.alpha = v);
        GetComponent<RectTransform>().DOScale(new Vector3(0.8f, 0.8f, 1f), 0.1f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutSine).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void Follow(Vector3 position)
    {
        transform.DOMove(position, followTime).SetEase(Ease.InOutSine);
    }
}
