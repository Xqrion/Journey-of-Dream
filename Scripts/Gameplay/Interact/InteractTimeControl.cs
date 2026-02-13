using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractTimeControl : Interactable
    {
        public GameTime timeIndex = 0;
        protected override void InteractAction()
        {
            base.InteractAction();
            if (Input.GetKeyDown(KeyCode.F))
            {
                int index = (int)timeIndex;
                index = (index + 1) % 3;
                timeIndex = (GameTime)index;
                TimeManager.instance.UpdateCurrentTime(timeIndex);
            }
        }
    }
}
