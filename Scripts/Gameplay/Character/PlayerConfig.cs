using UnityEngine;

namespace MyGameplay.Character
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        public string playerName;
        public string version;
    }
}
