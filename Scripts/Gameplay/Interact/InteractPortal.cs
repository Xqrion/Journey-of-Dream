using DG.Tweening;
using MyGameSystem.Manager;
using MyGameSystem.Scene;
using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractPortal : Interactable
    {
        public SceneType targetScene;
        public string transitionText = "场景加载 i n g";
        public PortalType portalType;
        public ParticleSystem[] particles;
        public Transform square;


        protected override void InteractAction()
        {
            base.InteractAction();
            if (Input.GetKeyDown(KeyCode.F))
            {
                //加载场景
                Debug.Log("准备加载场景");
                UIManager.instance.ClosePanel(UIConst.BarPanel);
                SceneLoader.instance.LoadScene(targetScene, transitionText);
                Destroy(gameObject);
            }
        }

        public void PlayParticle()
        {
            switch (portalType)
            {
                case PortalType.Particle:
                    foreach (var particle in particles)
                    {
                        particle.Play();
                    }
                    break;
                case PortalType.Square:
                    square.DOMoveY(1f, 1f).SetEase(Ease.InOutSine);

                    break;
            }

        }
    }

    public enum PortalType
    {
        Particle, Square, None
    }
}