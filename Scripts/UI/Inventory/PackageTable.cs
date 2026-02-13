using System.Collections.Generic;
using MyUI.Inventory.Item;
using UnityEngine;

namespace MyUI.Inventory
{
    [CreateAssetMenu(menuName = "Package/PackageTable", fileName = "PackageTable")]
    public class PackageTable : ScriptableObject
    {
        public List<PackageTableItem> DataList = new();


        public void AddPackageItem(PackageTableItem item)
        {
            DataList.Add(item);
        }

        public void RemovePackageItem(PackageTableItem removeItem)
        {
            foreach (PackageTableItem item in DataList)
            {
                if (item == removeItem)
                {
                    DataList.Remove(item);
                    Debug.Log("[name]为" + item.itemName + "已被删除");
                    return;
                }
            }
        }

        public bool FindPackageItem(PackageTableItem item) => DataList.Contains(item);

        public void ClearPackageItem()
        {
            DataList.Clear();
        }
    }
}





