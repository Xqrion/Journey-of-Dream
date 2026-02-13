using MyGameplay.Character;
using MyGameSystem.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace MyGameSystem.Manager
{
    public class SaveManager : PersistentSingleton<SaveManager>
    {
        public PlayerConfig playerConfig;

        [FormerlySerializedAs("IsFinish")] public bool IsStart;
        public bool IsFinish;

        const string PLAYER_DATA_START_KEY = "PlayerData_Start";
        const string PLAYER_DATA_FINISH_KEY = "PlayerData_Finish";
        const string PLAYER_SCENE_KEY = "Scene_";

        public void SaveScene()
        {
            for (var i = 0; i < UIManager.instance.GetSceneCellSequence().sceneSequence.Count; i++)
            {
                var sceneName = PLAYER_SCENE_KEY + i;
                PlayerPrefs.SetInt(sceneName,
                    UIManager.instance.GetSceneCellSequence().sceneSequence[i].isFinished ? 1 : 0);
            }
        }

        public bool LoadSceneByIndex(int sceneIndex)
        {
            string sceneName =PLAYER_SCENE_KEY + sceneIndex;
            if (!PlayerPrefs.HasKey(sceneName))
            {
                Debug.LogError("没有场景" + sceneName);
            }
            return PlayerPrefs.GetInt(sceneName, 0) == 1;
        }

        public void Save()
        {
            //SaveSystem.SaveByPlayerPrefs(PLAYER_DATA_KEY, _data);
            PlayerPrefs.SetInt(PLAYER_DATA_START_KEY, IsStart ? 1 : 0);
            PlayerPrefs.SetInt(PLAYER_DATA_FINISH_KEY, IsFinish ? 1 : 0);
        }
        public void Load()
        {
            // var json = SaveSystem.LoadFromPlayerPrefs(PLAYER_DATA_KEY);
            // _data = JsonUtility.FromJson<SaveData>(json);

            //GameManager.instance.playerName = saveData.playerName;
            IsStart = PlayerPrefs.GetInt(PLAYER_DATA_START_KEY, 0) == 1;
            IsFinish = PlayerPrefs.GetInt(PLAYER_DATA_FINISH_KEY, 0) == 1;
        }

    }
}
