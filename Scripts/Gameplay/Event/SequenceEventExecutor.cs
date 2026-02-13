using System;
using UnityEngine;

namespace MyGameplay.Event
{
    [CreateAssetMenu(fileName = "Executor_", menuName = "Event/Sequence Excutor")]
    public class SequenceEventExecutor : ScriptableObject
    {
        private Action<bool> OnFinished;
        private int _index;
        public EventNodeBase[] nodes;

        public void Init(Action<bool> onFinishedEvent)
        {
            _index = 0;

            foreach (EventNodeBase item in nodes)
            {
                item.Init(OnNodeFinished);
            }

            OnFinished = onFinishedEvent;
        }

        private void OnNodeFinished(bool success)
        {
            if (success)
            {
                //Debug.Log("准备执行下一个node,index=" + _index);
                ExecuteNextNode();
            }
            else
            {
                Debug.Log("Node " + _index + "fail!");
                OnFinished(false);
            }
        }

        private void ExecuteNextNode()
        {

            if (_index < nodes.Length)
            {
                if (nodes[_index].state == NodeState.Waiting)
                {
                    _index++;
                    nodes[_index - 1].Execute();
                }

            }
            else
            {
                //Debug.Log("sequence done");
                OnFinished(true);
            }
        }

        public void Execute()
        {
            foreach (var node in nodes)
            {
                node.state = NodeState.Waiting;
            }
            _index = 0;
            ExecuteNextNode();
        }
    }
}
