using MyGameSystem.Manager;
using UnityEngine;

namespace MyGameplay.Event
{
    [CreateAssetMenu(fileName = "Node_", menuName = "Event/Function/Able Player Input")]
    public class EN_AblePlayerInput : EventNodeBase
    {
        [SerializeField] private bool ablePlayerInput;

        public override void Execute()
        {
            base.Execute();
            GameManager.instance.AblePlayerInputPrior(ablePlayerInput);

            Debug.Log("功能节点已执行");
            state = NodeState.Finished;
            OnFinished(true);
        }
    }
}
