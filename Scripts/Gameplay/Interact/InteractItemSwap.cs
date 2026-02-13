using MyGameSystem.Manager;
using MyUI.Inventory.Item;
using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractItemSwap : Interactable
    {
        [SerializeField] protected PackageTableItem targetItem;
        [SerializeField] private PackageTableItem[] conditionItems;

        protected override void InteractAction()
        {
            base.InteractAction();



            if(Input.GetKeyDown(KeyCode.F))
            {
                foreach (var item in conditionItems)
                {
                    if (!UIManager.instance.GetPackageTable().FindPackageItem(item))
                    {
                        UIManager.SendTip(UIManager.instance.GetPackageTable().FindPackageItem(targetItem)
                            ? "-建好回来干嘛-"
                            : "-材料不全-");
                        return;
                    }

                }
                UIManager.SendTip("已获得" + targetItem.itemName);
                foreach (var item in conditionItems)
                {
                    UIManager.instance.GetPackageTable().RemovePackageItem(item);
                }
                UIManager.instance.GetPackageTable().AddPackageItem(targetItem);

                CloseTipMessage();

            }

        }
    }
}
