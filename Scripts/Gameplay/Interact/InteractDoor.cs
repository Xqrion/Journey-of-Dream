using DG.Tweening;
using MyGameSystem.Manager;
using MyUI.Inventory.Item;
using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractDoor : Interactable
    {
        [SerializeField] private PackageTableItem[] conditionItems;
        [SerializeField] private Transform door;
        protected override void InteractAction()
        {
            foreach (var item in conditionItems)
            {
                if (!UIManager.instance.GetPackageTable().FindPackageItem(item))
                    return;
            }

            base.InteractAction();
            if (Input.GetKeyDown(KeyCode.F))
            {
                CloseTipMessage();
                door.DOLocalRotate(new Vector3(0, -110f, 0), 2f).SetEase(Ease.Linear).
                    OnComplete(() => Destroy(gameObject));
                door.GetComponent<MeshCollider>().enabled = false;
            }

        }
    }
}
