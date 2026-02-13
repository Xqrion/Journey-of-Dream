using MyGameSystem.Manager;
using UnityEngine;

namespace MyGameplay.Event
{
    [CreateAssetMenu(fileName = "Node_", menuName = "Event/Message/Close Dialogue Box")]
    public class EN_CloseDialogueBox : EventNodeBase
    {
        public override void Execute()
        {
            base.Execute();
            UIManager.instance.CloseDialogueBox();
            state = NodeState.Finished;
            OnFinished(true);
        }
    }
}
