using System;
using MyGameSystem.Manager;
using UnityEngine;

namespace MyGameplay.Interact
{
    [RequireComponent(typeof(SphereCollider))]
    public class Interactable : MonoBehaviour
    {
        private SphereCollider _interactRange;

        [Header("Interact Tip")]
        [SerializeField] protected bool showTip = true;
        [SerializeField] protected string tipMessage;
        [SerializeField] protected bool showIcon = true;

        public Action Interaction;
        protected bool canInteract;

        protected virtual void Awake()
        {
            _interactRange = GetComponent<SphereCollider>();
            _interactRange.isTrigger = true;
        }

        protected virtual void Update()
        {
            if (!canInteract) return;

            InteractAction();

        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (showTip)
            {
                ShowTipMessage(tipMessage);
            }
            canInteract = true;
        }

        protected virtual void OnTriggerStay(Collider other)
        {
            if (showTip)
                UIManager.SustainInteractTip();
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (showTip)
                CloseTipMessage();
            canInteract = false;
        }

        private void ShowTipMessage(string message)
        {
            UIManager.ShowInteractTip(message, true, showIcon);
        }
        protected void CloseTipMessage()
        {
            UIManager.ShowInteractTip(null, false);
        }

        private void OnDestroy()
        {
            CloseTipMessage();
        }

        protected virtual void InteractAction()
        {
        }

    }
}
