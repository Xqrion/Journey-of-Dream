using DG.Tweening;
using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractMemoryTrigger : Interactable
    {
        protected override void InteractAction()
        {
            base.InteractAction();

            Interaction?.Invoke();

        }
        private void OnEnable()
        {
            GetComponent<RectTransform>().DOAnchorPosY(1f, 1f).SetEase(Ease.InSine);
        }
    }
}
