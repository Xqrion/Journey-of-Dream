using System;
using System.Collections.Generic;
using MyGameplay.Event;
using MyGameSystem.Core;
using MyUI.Inventory;
using MyUI.SceneMap;
using MyUI.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameSystem.Manager
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private DialogueBox _dialogueBox;
        [SerializeField] private TipBox _tipBox;
        [SerializeField] private InteractTip _interactTip;

        [SerializeField] private PackageTable packageTable;
        [SerializeField] private SceneCellSequence sceneCellSequence;

        private Transform UIRoot
        {
            get
            {
                if (_uiRoot == null)
                {
                    _uiRoot = GameObject.Find("Canvas") ? GameObject.Find("Canvas").transform : new GameObject("Canvas").transform;
                }
                return _uiRoot;
            }
        }

        private Transform _uiRoot;
        // 路径配置字典
        private Dictionary<string, string> pathDict;
        // 预制件缓存字典
        private Dictionary<string, GameObject> prefabDict;
        // 已打开界面的缓存字典
        private Dictionary<string, BasePanel> panelDict;

        public PackageTable GetPackageTable()
        {
            if (packageTable == null)
            {
                Debug.LogError("don't have PackageTable!");
            }
            return packageTable;
        }
        public SceneCellSequence GetSceneCellSequence()
        {
            if (sceneCellSequence == null)
            {
                Debug.LogError("don't have SceneSequence!");
            }
            return sceneCellSequence;
        }

        private UIManager()
        {
            InitDicts();
        }

        protected override void Awake()
        {
            base.Awake();
            InitUIElements();

        }


        private void InitUIElements()
        {
            var dialogueBoxPrefab = Resources.Load<GameObject>("Prefab/OtherUI/DialogueBox");
            var interactTipPrefab = Resources.Load<GameObject>("Prefab/OtherUI/InteractTip");
            var tipBoxPrefab = Resources.Load<GameObject>("Prefab/OtherUI/TipBox");

            var dialogueObject = Instantiate(dialogueBoxPrefab, UIRoot, false);

            _dialogueBox = dialogueObject.GetComponent<DialogueBox>();

            var interactObject = Instantiate(interactTipPrefab, UIRoot, false);
            _interactTip = interactObject.GetComponent<InteractTip>();

            var tipObject = Instantiate(tipBoxPrefab, UIRoot, false);
            _tipBox = tipObject.GetComponent<TipBox>();
        }

        #region Dialogue
        public void OpenDialogueBox(Action<bool> onNextEvent, int boxStyle = 0)
        {
            instance._dialogueBox.Open(onNextEvent, boxStyle);
        }

        public void CloseDialogueBox()
        {
            instance._dialogueBox.Close(null);
        }

        public static void PrintDialogue(DialogueData data)
        {
            if (data.speaker == "$$$")
            {
                data.speaker = GameManager.instance.PlayerName;
            }
            instance._dialogueBox.StartCoroutine(instance._dialogueBox.PrintDialogue(
                data.content,
                data.speaker,
                data.needTyping,
                data.autoNext,
                data.canQuickShow
            ));//TODO:改良函数
        }

        #endregion

        #region Tips

        public static void SendTip(string content) => instance._tipBox.ShowGameTip(content);

        public static void ShowInteractTip(string content, bool isShow, bool showIcon = true, string iconText = "F")
        {
            if (isShow)
            {
                instance._interactTip.ShowInteractTip(content, showIcon, iconText);
            }
            else
            {
                instance._interactTip.HideInteractTip();
            }
        }

        public static void SustainInteractTip() => instance._interactTip.SustainInteractTip();


        #endregion

        #region Panel

        private void InitDicts()
        {
            prefabDict = new Dictionary<string, GameObject>();
            panelDict = new Dictionary<string, BasePanel>();

            pathDict = new Dictionary<string, string>()
            {
                {UIConst.PackagePanel, "Package/PackagePanel"},
                {UIConst.BarPanel,"Player/BarPanel" },
                {UIConst.PausePanel,"Player/PausePanel"},
                {UIConst.StartOptionPanel, "Performance/StartOptionPanel"},
                {UIConst.StartInfoPanel, "Performance/StartInfoPanel"},
                {UIConst.MapNavigationPanel, "Player/MapNavigationPanel"},
                {UIConst.TimelinePanel, "Player/TimelinePanel"},
            };
        }

        public BasePanel GetPanel(string panelName)
        {
            // 检查是否已打开
            return panelDict.GetValueOrDefault(panelName);
        }

        public bool IsPanelOpen(string panelName) => panelDict.TryGetValue(panelName, out _);

        public BasePanel OpenPanel(string panelName)
        {
            // 检查是否已打开
            if (panelDict.TryGetValue(panelName, out var panel))
            {
                Debug.Log("界面已打开: " + panelName);
                return null;
            }

            // 检查路径是否配置
            if (!pathDict.TryGetValue(panelName, out var path))
            {
                Debug.Log("界面名称错误，或未配置路径: " + panelName);
                return null;
            }

            // 使用缓存预制件
            if (!prefabDict.TryGetValue(panelName, out var panelPrefab))
            {
                string realPath = "Prefab/Panel/" + path;

                panelPrefab = Resources.Load<GameObject>(realPath);
                prefabDict.Add(panelName, panelPrefab);
            }

            // 打开界面
            //var panelCanvas = Instantiate()
            var panelObject = Instantiate(panelPrefab, UIRoot, false);
            panel = panelObject.GetComponent<BasePanel>();
            if (!panel) Debug.LogError("没有给Panel挂载脚本");
            panelDict.Add(panelName, panel);
            panel.OnPanelOpen(panelName);
            return panel;
        }

        public bool ClosePanel(string panelName)
        {
            if (!panelDict.Remove(panelName, out var panel))
            {
                Debug.Log("界面未打开: " + panelName);
                return false;
            }

            panel.OnPanelClose();
            return true;
        }

        #endregion

        #region Button

        private Selectable _currentSelectable;
        public static void SetCurrentSelectable(Selectable obj)
        {
            instance._currentSelectable = obj;
        }

        #endregion
    }

    public struct UIConst
    {
        public const string PackagePanel = "PackagePanel";
        public const string BarPanel = "BarPanel";
        public const string PausePanel = "PausePanel";

        public const string StartOptionPanel = "StartOptionPanel";
        public const string StartInfoPanel = "StartInfoPanel";

        public const string MapNavigationPanel = "MapNavigationPanel";

        public const string TimelinePanel = "TimelinePanel";
    }
}