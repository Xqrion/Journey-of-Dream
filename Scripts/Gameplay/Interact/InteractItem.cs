using MyGameSystem.Manager;
using MyUI.Inventory.Item;
using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractItem : Interactable
    {
        [Header("Item")]
        [SerializeField] protected GameObject model;
        [SerializeField] protected ParticleSystem glowParticle;
        [Space(10)]
        [SerializeField] protected PackageTableItem targetItem;
        private OutlineEffect _outlineEffect;

        protected override void Awake()
        {
            base.Awake();
            _outlineEffect = model.AddComponent<OutlineEffect>();
            _outlineEffect.OutlineMode = OutlineEffect.Mode.OutlineAll;
            _outlineEffect.enabled = false;
            _outlineEffect.OutlineColor = Color.cyan;
            _outlineEffect.OutlineWidth = 10f;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            _outlineEffect.enabled = true;
            //glowParticle.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.OutBounce);
        }


        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            _outlineEffect.enabled = false;
            //glowParticle.gameObject.SetActive(true);
        }

        protected override void InteractAction()
        {
            base.InteractAction();
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (targetItem != null)
                {
                    UIManager.instance.GetPackageTable().AddPackageItem(targetItem);
                    UIManager.SendTip("已获得" + targetItem.itemName);
                }
                CloseTipMessage();
                Destroy(gameObject);
            }
        }
    }
}
