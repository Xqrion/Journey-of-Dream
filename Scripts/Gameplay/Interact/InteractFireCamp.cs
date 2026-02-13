using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractFireCamp : Interactable
    {
        public ParticleSystem fire;
        public ParticleSystem firework;
        public ParticleSystem fireExplosion;
        private AudioSource _fireSound;


        protected override void Awake()
        {
            base.Awake();
            _fireSound = GetComponent<AudioSource>();
        }

        protected override void InteractAction()
        {
            base.InteractAction();

            if (Input.GetKeyDown(KeyCode.F))
            {
                fire.Play();
                firework.Play();
                fireExplosion.Play();
                if (!_fireSound.isPlaying)
                    _fireSound.Play();
            }
        }
    }
}
