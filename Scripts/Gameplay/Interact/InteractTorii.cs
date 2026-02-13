using MyGameSystem.Manager;
using MyUI.Inventory.Item;
using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractTorii : Interactable
    {
        public PackageTableItem conditionItem;

        public ParticleSystem particle;
        public InteractPortal portal;
        protected override void InteractAction()
        {
            if (!UIManager.instance.GetPackageTable().FindPackageItem(conditionItem))
                return;

            base.InteractAction();
            if (Input.GetKeyDown(KeyCode.F))
            {

                if (SaveManager.instance.IsFinish)
                {
                    particle.Play();
                    CloseTipMessage();
                    portal.gameObject.SetActive(true);
                }
                else
                {
                    UIManager.SendTip("-请在通关后来-");
                }
            }

        }

    }
}
