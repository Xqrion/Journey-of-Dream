using MyGameSystem.Manager;
using MyUI.Inventory.Item;
using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractCDPlayer : Interactable
    {
        [SerializeField] private PackageTableItem requiredDisc;
        [SerializeField] private AudioClip clip;
        [SerializeField] private float volume = 1f;
        [SerializeField] private bool isPlay;

        protected override void InteractAction()
        {
            base.InteractAction();
            if (!UIManager.instance.GetPackageTable().FindPackageItem(requiredDisc)) return;
            if (isPlay) return;

            if (Input.GetKeyDown(KeyCode.F))
            {
                AudioManager.instance.StopBGM();
                AudioManager.instance.PlaySFX(clip, volume);
                UIManager.SendTip("-正在播放ing-");
                isPlay = !isPlay;
            }
        }
    }
}
