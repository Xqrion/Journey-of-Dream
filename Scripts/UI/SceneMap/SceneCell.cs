using MyGameSystem.Manager;
using UnityEngine;

namespace MyUI.SceneMap
{
    [CreateAssetMenu(menuName = "Map/Scene Cell")]
    public class SceneCell : ScriptableObject
    {
        public SceneType sceneType;
        public string imageName;

        public string sceneTitle;
        [Multiline] public string sceneSubtitle;

        [Space]
        public bool isFinished;
    }
}
