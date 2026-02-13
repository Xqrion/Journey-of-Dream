using DG.Tweening;
using MyGameSystem.Manager;
using MyGameSystem.Scene;
using UnityEngine;

namespace MyUI.Performance
{
    public class StartGate : MonoBehaviour
    {
        public Transform cameraTransform;

        private void Awake()
        {
            if (cameraTransform == null)
            {
                if (Camera.main != null) cameraTransform = Camera.main.transform;
            }
        }

        public void EnterStartGate()
        {

            transform.DOMoveY(1f, 2f).SetEase(Ease.OutSine).onComplete += () =>
            {
                //SceneLoader.instance.PrepareScene();
            };
            // DOVirtual.Float(0f, 1f, 1f, v =>
            // {
            //     startPanel.alpha = v;
            // }).SetEase(Ease.OutSine).SetDelay(3f);

            cameraTransform.DOMove(new Vector3(120.5f, 1f, 10f), 2f).SetEase(Ease.InOutSine).SetDelay(3f).onComplete += () =>
            {
                SceneLoader.instance.LoadScene(SceneType.Gameplay1, "佩戴耳机效果更佳哦！");
            };

        }
    }
}
