using MyGameSystem.Manager;

namespace MyGameplay.Interact
{
    public class InteractConditionTrigger : InteractTrigger
    {
        private bool enableAction;

        protected override void Awake()
        {
            base.Awake();
            EventManager.instance.RegisterEvent("ConditionTrigger", (o, param1, param2) =>
            {
                enableAction = true;
            });
        }

        protected override void InteractAction()
        {
            if (!enableAction) return;
            base.InteractAction();
        }
    }
}
