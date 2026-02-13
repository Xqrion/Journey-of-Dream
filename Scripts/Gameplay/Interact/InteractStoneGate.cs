using MyGameSystem.Manager;
using MyUI.Inventory.Item;
using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractStoneGate : Interactable
    {
        [SerializeField] private InteractConditionTrigger trigger;
        [SerializeField] private PackageTableItem[] conditionItems;
        [SerializeField] private GameObject gate;
        //[SerializeField] private ParticleSystem particle;

        protected override void InteractAction()
        {
            base.InteractAction();


            if (Input.GetKeyDown(KeyCode.F))
            {
                foreach (var item in conditionItems)
                {
                    if (!UIManager.instance.GetPackageTable().FindPackageItem(item))
                    {
                        UIManager.SendTip("-材料不足-");
                        return;
                    }
                }

                UIManager.SendTip("-建造成功-");
                EventManager.instance.TriggerEvent("ConditionTrigger", null, 0, 0);
                gate.SetActive(true);
                //particle.Play();

                Destroy(this);
            }

        }
    }
}
