using MyGameplay.Interact;
using MyGameSystem.Manager;
using UnityEngine;

namespace MyGameplay.Event
{
    [CreateAssetMenu(fileName = "Node_", menuName = "Event/Function/Open Portal")]
    public class EN_OpenPortal : EventNodeBase
    {
        public override void Execute()
        {
            base.Execute();
            GameManager.instance.portal.gameObject.SetActive(true);
            GameManager.instance.portal.GetComponent<InteractPortal>().PlayParticle();
            Debug.Log("功能节点已执行");
            state = NodeState.Finished;
            OnFinished(true);
        }
    }
}
