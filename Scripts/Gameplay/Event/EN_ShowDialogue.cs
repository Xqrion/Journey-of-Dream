using System;
using MyGameSystem.Manager;
using UnityEngine;

namespace MyGameplay.Event
{
    [Serializable]
    public class DialogueData
    {
        public string speaker;
        [Multiline] public string content;
        public bool autoNext;
        public bool needTyping = true;
        public bool canQuickShow = true;
    }
    [CreateAssetMenu(fileName = "Node_", menuName = "Event/Message/Show Dialogue")]
    public class EN_ShowDialogue : EventNodeBase
    {
        public DialogueData[] datas;
        public int boxStyle = 0;
        private int _index;

        public override void Execute()
        {
            base.Execute();
            _index = 0;
            UIManager.instance.OpenDialogueBox(ShowNextDialogue, boxStyle);
        }

        private void ShowNextDialogue(bool forDisplayDirectly)
        {
            if (_index < datas.Length)
            {
                DialogueData data = new DialogueData()
                {
                    speaker = datas[_index].speaker,
                    content = datas[_index].content,
                    canQuickShow = datas[_index].canQuickShow,
                    autoNext = datas[_index].autoNext,
                    needTyping = !forDisplayDirectly && datas[_index].needTyping
                };
                UIManager.PrintDialogue(data);
                _index++;
            }
            else
            {
                state = NodeState.Finished;
                OnFinished(true);
                //Debug.Log("这个node已经执行完");
            }
        }
    }
}