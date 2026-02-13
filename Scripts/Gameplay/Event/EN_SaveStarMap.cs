using System.Collections;
using System.Collections.Generic;
using MyGameplay.Event;
using MyGameSystem.Manager;
using UnityEngine;

[CreateAssetMenu(fileName = "Node_", menuName = "Event/Function/Save StarMap")]
public class EN_SaveStarMap : EventNodeBase
{
    public override void Execute()
    {
        base.Execute();

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

        Debug.Log("功能节点已执行");
        state = NodeState.Finished;
        OnFinished(true);
    }
}
