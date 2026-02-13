using MyGameSystem.Manager;
using UnityEngine;

namespace MyGameplay.Interact
{
    public class InteractStarMap : Interactable
    {
        protected override void InteractAction()
        {
            base.InteractAction();

            if (!Input.GetKeyDown(KeyCode.F)) return;

            switch (GameManager.instance.CurrentScene)
            {
                case SceneType.Gameplay1:
                    UIManager.instance.GetSceneCellSequence().RefreshFinishedScene(0);
                    break;
                case SceneType.Gameplay2:
                    UIManager.instance.GetSceneCellSequence().RefreshFinishedScene(1);
                    break;
                case SceneType.Gameplay4:
                    UIManager.instance.GetSceneCellSequence().RefreshFinishedScene(2);
                    break;
            }
            CloseTipMessage();
            UIManager.SendTip("新的星图已解锁");
            Destroy(gameObject);

        }
    }
}
