using MyGameSystem.Manager;
using MyUI.Inventory.Item;
using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractConditionItem : InteractItem
    {
        [SerializeField] private PackageTableItem conditionItem;

        protected override void InteractAction()
        {
            if (!UIManager.instance.GetPackageTable().FindPackageItem(conditionItem))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    UIManager.SendTip("-工具不足-");
                }
                return;
            }
            base.InteractAction();
        }
    }
}
