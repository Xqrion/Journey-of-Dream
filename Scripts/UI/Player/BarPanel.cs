
using DG.Tweening;
using MyGameSystem.Manager;
using UnityEngine;
using UnityEngine.UI;

public class BarPanel : BasePanel
{
    [SerializeField] private Button settingButton;
    [SerializeField] private Button packageButton;
    [SerializeField] private Button sceneMapButton;


    private RectTransform myTransform;

    public override void OnPanelOpen(string openPanelName)
    {
        base.OnPanelOpen(openPanelName);
        myTransform.anchoredPosition = new Vector3(0, 150, 0);
        myTransform.DOAnchorPosY(0, 2f).SetEase(Ease.OutSine);
    }

    protected override void Awake()
    {
        myTransform = GetComponent<RectTransform>();

        // settingButton = transform.Find("SettingButton").GetComponent<Button>();
        // packageButton = transform.Find("BagButton").GetComponent<Button>();

        settingButton.onClick.AddListener(() =>
        {
            UIManager.instance.OpenPanel(UIConst.PausePanel);
        });

        packageButton.onClick.AddListener(() =>
        {
            UIManager.instance.OpenPanel(UIConst.PackagePanel);
        });

        sceneMapButton.onClick.AddListener(() =>
        {
            UIManager.instance.OpenPanel(UIConst.MapNavigationPanel);
        });
    }


    protected override void HandleInput()
    {
        if (UIManager.instance.IsPanelOpen(UIConst.PausePanel) ||
            UIManager.instance.IsPanelOpen(UIConst.PackagePanel) ||
            UIManager.instance.IsPanelOpen(UIConst.MapNavigationPanel)) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            UIManager.instance.OpenPanel(UIConst.PackagePanel);
            //enableInput = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.instance.OpenPanel(UIConst.PausePanel);
            //enableInput = false;
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            UIManager.instance.OpenPanel(UIConst.MapNavigationPanel);
            //enableInput = false;
        }


    }
}
