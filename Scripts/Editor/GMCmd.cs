using MyGameSystem.Manager;
using MyGameSystem.Scene;
using MyUI.Inventory;
using MyUI.Inventory.Item;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class GMCmd
    {
        [MenuItem("GMCmd/Inventory/Read the Table")]
        public static void ReadTable()
        {
            PackageTable packageTable = UIManager.instance.GetPackageTable();
            foreach (PackageTableItem item in packageTable.DataList)
            {
                Debug.Log(string.Format("[name]:{0}", item.itemName));
            }
        }

        [MenuItem("GMCmd/Inventory/Open Package")]
        public static void OpenPackagePanel()
        {
            UIManager.instance.OpenPanel(UIConst.PackagePanel);
        }

        [MenuItem("GMCmd/Inventory/Clear Package")]
        public static void ClearPackagePanel()
        {
            UIManager.instance.GetPackageTable().ClearPackageItem();
        }

        [MenuItem("GMCmd/Transform to Hidden")]
        public static void GetCandy()
        {
            UIManager.instance.ClosePanel(UIConst.BarPanel);
            UIManager.instance.ClosePanel(UIConst.PausePanel);
            SceneLoader.instance.LoadScene(SceneType.Gameplay3);
        }

        [MenuItem("GMCmd/Save System/Delete Player Data")]
        public static void DeletePlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            SaveManager.instance.IsStart = false;
        }

        [MenuItem("GMCmd/Save System/Set Player Started")]
        public static void SetPlayerStarted()
        {
            SaveManager.instance.IsStart = true;
            SaveManager.instance.Save();
        }

        [MenuItem("GMCmd/Save System/Save Player Finished Scene")]
        public static void SetPlayerSceneFinished()
        {
            SaveManager.instance.SaveScene();

        }

    }
}
