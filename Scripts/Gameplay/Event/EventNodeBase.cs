using System;
using UnityEngine;

namespace MyGameplay.Event
{
    public enum NodeState
    {
        Waiting,
        Execuing,
        Finished
    }
    public class EventNodeBase : ScriptableObject
    {
        protected Action<bool> OnFinished;//执行完带着是否成功返回
        [HideInInspector] public NodeState state;

        public virtual void Init(Action<bool> onFinishedEvent)
        {
            OnFinished = onFinishedEvent;
            state = NodeState.Waiting;
        }

        public virtual void Execute()
        {
            if (state != NodeState.Waiting)
                return;
            state = NodeState.Execuing;
        }
    }
}