using MyGameSystem.Manager;
using MyUI.Inventory.Item;
using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractStone : Interactable
    {
        [SerializeField] private PackageTableItem conditionItems;
        [SerializeField] private GameObject[] particles;

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            UIManager.SendTip("-正在录音中-");
        }

        protected override void InteractAction()
        {
            if (!UIManager.instance.GetPackageTable().FindPackageItem(conditionItems))
                return;

            base.InteractAction();
            if (Input.GetKeyDown(KeyCode.F))
            {
                foreach (var particle in particles)
                {
                    particle.SetActive(true);
                    particle.GetComponent<ParticleSystem>().Play();
                }
                GameManager.instance.ChangeWeather(WeatherType.Snowy);
            }

        }
    }
}
