using MyUI.Inventory.Item;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyUI.Inventory
{
    public class PackageCell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private static readonly int In = Animator.StringToHash("In");
        private static readonly int Out = Animator.StringToHash("Out");
        private Transform UIIcon;
        private Transform UISelect;

        public bool OnSelect
        {
            get => UISelect.gameObject.activeSelf; set => UISelect.gameObject.SetActive(value);
        }

        private Transform UIName;
        private Transform _uiMouseOverAni;

        public PackageTableItem packageTableItem;
        private PackagePanel _uiParent;

        private void Awake()
        {
            InitUIName();
        }
        private void InitUIName()
        {
            UIIcon = transform.Find("Icon");
            UISelect = transform.Find("Select");
            UIName = transform.Find("MouseOver/Name");
            _uiMouseOverAni = transform.Find("MouseOver");
        }

        public void Refresh(PackageTableItem item, PackagePanel uiParent)
        {
            packageTableItem = item;
            Texture2D t = (Texture2D)Resources.Load("Art/Texture/" + item.imageName);
            Sprite s = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
            UIIcon.GetComponent<Image>().sprite = s;
            this._uiParent = uiParent;
            UIName.GetComponent<TextMeshProUGUI>().text = packageTableItem.itemName;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // Debug.Log("OnPointerClick: " + eventData.ToString());
            _uiParent.RefreshDetail(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Debug.Log("OnPointerEnter: " + eventData.ToString());
            _uiMouseOverAni.GetComponent<Animator>().SetTrigger(In);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // Debug.Log("OnPointerExit: " + eventData.ToString());
            _uiMouseOverAni.GetComponent<Animator>().SetTrigger(Out);
        }


    }
}
