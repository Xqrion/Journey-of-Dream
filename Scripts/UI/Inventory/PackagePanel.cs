using DG.Tweening;
using MyGameSystem.Manager;
using MyUI.Inventory.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyUI.Inventory
{
    public class PackagePanel : BasePanel
    {
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        private Transform UICloseBtn;
        private Transform UIScrollView;
        private Transform UIDetailPanel;
        private Transform UITitle;

        private PackageTable UIPackageTable;

        private PackageCell UICurrentCell;


        public GameObject PackageUIItemPrefab;

        protected override void Awake()
        {
            base.Awake();
            InitUI();
            UIPackageTable = UIManager.instance.GetPackageTable();
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        protected override void Start()
        {
            RefreshUI();

        }

        public override void OnPanelOpen(string openPanelName)
        {
            enableInput = false;

            base.OnPanelOpen(openPanelName);
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
            GameManager.instance.AblePlayerInput(true);
            GameManager.instance.AblePlayerCursor(false);
            _rectTransform.DOAnchorPosY(-900f, 0.5f).SetEase(Ease.InSine).OnComplete(
                () => base.OnPanelClose());
        }

        protected override void HandleInput()
        {
            base.HandleInput();

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                UIManager.instance.ClosePanel(UIConst.PackagePanel);
            }
        }

        private void InitUI()
        {
            InitUIName();
            InitClick();
        }

        private void RefreshUI()
        {
            RefreshScroll();
            RefreshTitle();
        }

        public void RefreshDetail(PackageCell packageCell)
        {
            if (UICurrentCell != null) UICurrentCell.OnSelect = false;
            UICurrentCell = packageCell;
            packageCell.OnSelect = true;
            // 找到uid对应的动态数据
            PackageTableItem localItem = UICurrentCell.packageTableItem;
            // 刷新详情界面
            UIDetailPanel.GetComponent<PackageDetail>().Refresh(localItem, this);
        }

        private void RefreshScroll()
        {
            // 清理滚动容器中原本的物品
            RectTransform scrollContent = UIScrollView.GetComponent<ScrollRect>().content;
            for (int i = 0; i < scrollContent.childCount; i++)
            {
                Destroy(scrollContent.GetChild(i).gameObject);
            }
            foreach (PackageTableItem data in UIManager.instance.GetPackageTable().DataList)
            {
                Transform PackageUIItem = Instantiate(PackageUIItemPrefab.transform, scrollContent);
                PackageCell packageCell = PackageUIItem.GetComponent<PackageCell>();
                packageCell.Refresh(data, this);
            }

        }

        private void RefreshTitle()
        {
            int number = UIPackageTable.DataList.Count;
            UITitle.GetComponent<TextMeshProUGUI>().SetText("已收集的物品:" + number + "/" + GameManager.instance.inventoryVolume);
        }

        private void InitUIName()
        {
            UICloseBtn = transform.Find("RightTop/Close");
            UITitle = transform.Find("RightTop/Title");
            UIScrollView = transform.Find("Center/Scroll View");
            UIDetailPanel = transform.Find("Center/DetailPanel");
        }

        private void InitClick()
        {
            UICloseBtn.GetComponent<Button>().onClick.AddListener(OnClickClose);
        }
        private static void OnClickClose()
        {
            UIManager.instance.ClosePanel(UIConst.PackagePanel);
        }

    }
}
