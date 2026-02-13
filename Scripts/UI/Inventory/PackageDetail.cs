using MyUI.Inventory.Item;
using TMPro;
using UnityEngine;

namespace MyUI.Inventory
{
    public class PackageDetail : MonoBehaviour
    {
        private Transform UIDescription;
        private Transform UITitle;
        private PackageTableItem packageTableItem;
        //private PackagePanel uiParent;

        private void Awake()
        {
            InitUIName();
        }

        private void InitUIName()
        {

            UIDescription = transform.Find("Bottom/Description");
            UITitle = transform.Find("Top/Title");

        }

        public void Refresh(PackageTableItem data, PackagePanel uiParent)
        {
            // 初始化：动态数据、静态数据、父物品逻辑
            packageTableItem = data;
            //this.uiParent = uiParent;
            UIDescription.GetComponent<TextMeshProUGUI>().text = packageTableItem.description;
            UITitle.GetComponent<TextMeshProUGUI>().text = "-" + packageTableItem.itemName + "-";

        }

    }
}
