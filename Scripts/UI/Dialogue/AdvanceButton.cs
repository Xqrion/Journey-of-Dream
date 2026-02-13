using MyGameplay.Scriptable;
using MyGameSystem.Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyUI.Dialogue
{
    public class AdvanceButton : Button
    {
        private AudioData _selectAudioData;

        protected override void Awake()
        {
            onClick.AddListener(OnClickEvent);
            _selectAudioData = Resources.Load<AudioData>("Art/Music/UIClick");
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            Select();
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            AudioManager.instance.PlaySFX(_selectAudioData);
            UIManager.SetCurrentSelectable(this);
        }
        //protected int index;
        protected virtual void OnClickEvent() { }

        // public Action<int> OnConfirm; // int参数代表自身的下标序号
        // public virtual void Init (string content, int index, Action<int> onConfirmEvent)
        // {
        //     _index = index;
        //     OnConfirm += onConfirmEvent;
        // }
        // public void Confirm()
        // {
        //     OnConfirm(_index);
        // }

    }
}
