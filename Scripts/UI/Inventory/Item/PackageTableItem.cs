using UnityEngine;

namespace MyUI.Inventory.Item
{
    [CreateAssetMenu(fileName = "Item_", menuName = "Package/Package Item")]
    public class PackageTableItem : ScriptableObject
    {
        //public int id;
        //public int type;//eumn?
        public string itemName;
        [Multiline]
        public string description;
        //public string skillDescription;
        public string imageName;
    }
}
