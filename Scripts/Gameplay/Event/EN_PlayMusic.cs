using MyGameSystem.Manager;
using UnityEngine;

namespace MyGameplay.Event
{
    [CreateAssetMenu(fileName = "Node_", menuName = "Event/Function/Play Music")]
    public class EN_PlayMusic : EventNodeBase
    {
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private float volume = 0.2f;
        [SerializeField] private float pitch = 1f;

        public override void Execute()
        {
            base.Execute();
            AudioManager.instance.StopBGM();
            AudioManager.instance.PlaySFX(audioClip, volume, pitch);

            Debug.Log("功能节点已执行");
            state = NodeState.Finished;
            OnFinished(true);
        }

    }
}
