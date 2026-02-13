using DG.Tweening;
using MyGameSystem.Manager;
using UnityEngine;

public class TimelinePanel : BasePanel
{
    private CanvasGroup _canvasGroup;

    [SerializeField] private CursorA cursorA;

    protected override void Awake()
    {
        base.Awake();

        _canvasGroup =  GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    protected override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.instance.StopTimeline();
        }
    }

    public override void OnPanelOpen(string openPanelName)
    {
        base.OnPanelOpen(openPanelName);

        cursorA.OpenCursor();

        DOVirtual.Float(0f, 1f, 0.5f, v => _canvasGroup.alpha = v).SetDelay(3f);
        DOVirtual.Float(1f, 0f, 0.5f, v => _canvasGroup.alpha = v).SetDelay(8f);

    }

}
