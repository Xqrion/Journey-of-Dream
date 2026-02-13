using MyGameSystem.Manager;
using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractTrigger : Interactable
    {
        private float _timer;
        [SerializeField] private ParticleSystem particle;

        protected override void Update()
        {
            base.Update();
            _timer += Time.deltaTime;
            _timer = Mathf.Clamp(_timer, 0f, 10f);

            //if (_timer >= 2f && !particle.isPlaying) particle.Play();
        }

        protected override void InteractAction()
        {
            if (!GameManager.instance.CurrentEventFinished || _timer <= 1.5f) return;
            //Debug.Log(1);
            Interaction?.Invoke();
            CloseTipMessage();

        }
    }
}
