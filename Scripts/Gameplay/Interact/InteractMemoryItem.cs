using MyGameplay.Scriptable;
using MyGameSystem.Manager;
using MyUI.Inventory.Item;
using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractMemoryItem : Interactable
    {
        [SerializeField] private PackageTableItem targetItem;
        [SerializeField] private ParticleSystem destroyParticle;
        private AudioData audioData;

        protected override void Awake()
        {
            base.Awake();
            audioData = Resources.Load<AudioData>("Art/Music/MemoryItemPick");
        }

        protected void Start()
        {
            if (targetItem == null) return;
            if (UIManager.instance.GetPackageTable().FindPackageItem(targetItem))
                Destroy(gameObject);
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
                destroyParticle.Play();
                AudioManager.instance.PlaySFX(audioData.clip,audioData.volume, audioData.pitch);
                Destroy(gameObject);
            }
        }
    }
}
