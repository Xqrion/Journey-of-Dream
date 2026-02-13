using System.Collections.Generic;
using MyGameSystem.Manager;
using UnityEngine;

namespace MyUI.SceneMap
{
    [CreateAssetMenu(fileName = "SceneCellSequence", menuName = "Map/Scene Cell Sequence")]
    public class SceneCellSequence : ScriptableObject
    {
        public List<SceneCell> sceneSequence = new();


        public void Refresh()
        {
            for (int i = 0; i < sceneSequence.Count; i++)
            {
                sceneSequence[i].isFinished = SaveManager.instance.LoadSceneByIndex(i);
            }
        }

        public void RefreshFinishedScene(int index)
        {
            sceneSequence[index].isFinished = true;
            SaveManager.instance.SaveScene();
        }


    }
}
