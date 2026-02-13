using DG.Tweening;
using MyGameSystem.Core;
using MyGameSystem.Manager;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    [SerializeField] private Vector3 morningRotation;
    [SerializeField] private Vector3 noonRotation;
    [SerializeField] private Vector3 nightRotation;
    public GameTime currentTime = GameTime.Noon;

    public void UpdateCurrentTime(GameTime time)
    {
        currentTime = time;
        switch (time)
        {
            case GameTime.Morning:
                transform.DORotate(morningRotation, 2f).SetEase(Ease.InOutSine).SetDelay(0.2f);
                UIManager.SendTip("当前为早晨");
                break;
            case GameTime.Noon:
                transform.DORotate(noonRotation, 2f).SetEase(Ease.InOutSine).SetDelay(0.2f);
                UIManager.SendTip("当前为中午");
                break;
            case GameTime.Night:
                transform.DORotate(nightRotation, 2f).SetEase(Ease.InOutSine).SetDelay(0.2f);
                if (GameManager.instance.CurrentWeather == WeatherType.Snowy)
                {
                    EventManager.instance.TriggerEvent("WeatherChange", null, 0, 0);
                }
                UIManager.SendTip("当前为晚上");
                break;

        }
    }
}

public enum GameTime
{
    Morning,
    Noon,
    Night,
}