using MyGameSystem.Manager;

namespace MyGameplay.Interact
{
    public class InteractWeatherTrigger : Interactable
    {
        private bool _flag;
        protected override void Awake()
        {
            base.Awake();
            EventManager.instance.RegisterEvent("WeatherChange", (o, param1, param2) =>
            {
                _flag = true;
            });
        }

        protected override void InteractAction()
        {
            base.InteractAction();
            if (!_flag) return;

            Interaction?.Invoke();
            CloseTipMessage();
        }

        // private void OnDestroy()
        // {
        //     CloseTipMessage();
        // }
    }
}
