using MyGameSystem.Manager;
using UnityEngine;

namespace MyGameplay.Event
{
    [CreateAssetMenu(fileName = "Node_", menuName = "Event/Function/Save PlayerData")]
    public class EN_SavePlayerData : EventNodeBase
    {
        [SerializeField] private bool isStart = true;
        [SerializeField] private bool isFinish = true;

        public override void Execute()
        {
            base.Execute();

            SaveManager.instance.IsStart = isStart || SaveManager.instance.IsStart;
            SaveManager.instance.IsFinish = isFinish || SaveManager.instance.IsFinish;
            SaveManager.instance.Save();

            Debug.Log("功能节点已执行");
            state = NodeState.Finished;
            OnFinished(true);
        }
    }
}
