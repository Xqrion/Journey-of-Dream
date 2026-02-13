using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using MyUI.Dialogue;
using UnityEngine.Serialization;

public class DialogueBox : MonoBehaviour
{
    [FormerlySerializedAs("_background")]
    [Header("---Components---")]
    [SerializeField] private Image background;
    [FormerlySerializedAs("_widget")] [SerializeField] private Widget widget;
    [FormerlySerializedAs("_speaker")] [SerializeField] private TextMeshProUGUI speaker;
    [FormerlySerializedAs("_content")] [SerializeField] private AdvancedText content;
    [FormerlySerializedAs("_cursor")] [SerializeField] private CursorA cursor;

    [FormerlySerializedAs("_backgroundStyles")]
    [Header("---Configs---")]
    [SerializeField] private Sprite[] backgroundStyles;

    private bool _interactable;
    private bool _printFinished;
    private bool _canQuickShow;
    private bool _autoNext;
    private float _nextInterval;//说不定有用

    private bool CanQuickShow => !_printFinished && _canQuickShow;
    private bool CanNext => _printFinished;

    public Action<bool> OnNext;//可以自定义下一句话的效果（这里是下一句要不要强制显示）

    private void Awake()
    {
        content.onFinished = PrintFinished;
        cursor.gameObject.SetActive(false);

    }

    private void PrintFinished()
    {
        if (_autoNext)
        {
            _interactable = false;
            OnNext(false);
        }
        else
        {
            _interactable = true;
            _printFinished = true;

            cursor.OpenCursor();

        }
    }

    private void Update()
    {
        if (_interactable)
        {
            UpdateInput();
        }
    }

    private void UpdateInput()
    {
        if (!Input.GetKeyDown(KeyCode.Return)) return;

        if (_printFinished)
            cursor.OnClick();

        //按下esc
        if (CanQuickShow)
        {
            content.QuickShowRemaining();
        }
        // else if (CanNext)
        // {
        //     _interactble = false;
        //     OnNext(true);
        // }
        else if (CanNext)
        {
            _interactable = false;
            OnNext(false);
        }
        // else if (Input.GetKeyDown(KeyCode.Return))
        // {
        //     //回车或空格
        //
        // }
    }

    public void Close(Action onClosed)
    {
        widget.Fade(0, 0.4f, () =>
        {
            onClosed?.Invoke();
            gameObject.SetActive(false);
        });
    }

    public void Open(Action<bool> nextEvent, int boxStyle = 0)
    {
        //_cursor.gameObject.SetActive(false);

        OnNext = nextEvent;
        background.sprite = backgroundStyles[boxStyle];

        if (gameObject != null && !gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            widget.RenderOpacity = 0;
            widget.Fade(1, 0.2f, null);
            speaker.SetText("");
            content.Initialize();
        }

        OnNext(false);
    }

    public IEnumerator PrintDialogue(string content, string speaker, bool needTyping = true,
    bool autoNext = false, bool CanQuickShow = true)
    {
        _interactable = false;
        _printFinished = false;

        if (this.content.text != "")
        {
            this.content.Disappear();
            yield return new WaitForSecondsRealtime(0.3f);//这里有点硬
        }

        _canQuickShow = CanQuickShow;
        _autoNext = autoNext;
        this.speaker.SetText(speaker);
        if (needTyping)
        {
            _interactable = true;
            this.content.StartCoroutine(this.content.SetText(content, AdvancedText.DisplayType.Typing, 0.2f, 0.05f));
        }
        else
        {
            this.content.StartCoroutine(this.content.SetText(content, AdvancedText.DisplayType.Fading, 0.2f, 0.05f));
        }
    }
}
