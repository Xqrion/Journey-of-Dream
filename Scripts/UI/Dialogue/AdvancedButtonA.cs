using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyUI.Dialogue
{
    public class AdvancedButtonA : AdvanceButton
    {
        private CursorA _cursorA;

        public void InitCursorA(CursorA cursorA)
        {
            _cursorA = cursorA;
        }


        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            GetComponent<RectTransform>().DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f);
            UpdateCursorA(transform.position);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            GetComponent<RectTransform>().DOScale(Vector3.one, 0.5f);
        }

        protected override void OnClickEvent()
        {
            base.OnClickEvent();
            _cursorA.OnClick();
        }

        private void UpdateCursorA(Vector3 position)
        {
            if (!_cursorA.gameObject.activeSelf)
            {
                _cursorA.OpenCursor();
                _cursorA.transform.position = position;
            }
            else
            {
                _cursorA.Follow(position);
            }
        }

    }
}
