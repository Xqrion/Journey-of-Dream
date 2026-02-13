using System.Linq;
using MyGameSystem.Manager;
using MyUI.Inventory.Item;
using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractBridge : Interactable
    {
        [SerializeField] private PackageTableItem[] conditionItems;
        [SerializeField] private GameObject bridgeWoods;
        private Transform[] _woods;
        private bool _isBuilt;

        protected override void Awake()
        {
            base.Awake();
            var tempWoods = bridgeWoods.GetComponentsInChildren<Transform>();
            _woods = new Transform[tempWoods.Length - 1];
            for (int i = 1; i < tempWoods.Length; i++)
            {
                _woods[i - 1] = tempWoods[i];
            }

            bridgeWoods.SetActive(false);
        }

        private void BuildBridge()
        {
            _isBuilt = true;
            bridgeWoods.SetActive(true);

            // foreach (var t in _woods)
            // {
            //     t.position += Vector3.up;
            // }
            // for (var i = 0; i < _woods.Length; i++)
            // {
            //     _woods[i].DOMoveY(_woods[i].position.y - 1f, 0.5f).SetDelay(0.1f * i + 0.1f);
            // }
        }

        protected override void InteractAction()
        {

            base.InteractAction();
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (conditionItems.Any(item => !UIManager.instance.GetPackageTable().FindPackageItem(item)))
                {
                    UIManager.SendTip(_isBuilt ? "-建好了还回来干嘛-" : "-材料不够-");

                    return;
                }

                GameManager.CloseAirWallBridge();
                bridgeWoods.SetActive(true);
                BuildBridge();
                UIManager.SendTip("-建造成功-");
                foreach (var item in conditionItems)
                {
                    UIManager.instance.GetPackageTable().RemovePackageItem(item);
                }

            }
        }
    }
}
