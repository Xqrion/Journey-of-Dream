using System;
using MyGameplay.Event;
using MyGameplay.Interact;
using MyGameSystem.Core;
using MyGameSystem.Scene;
using UnityEngine;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

namespace MyGameSystem.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        [Header("===玩家相关===")]

        [SerializeField] public IL3DN.IL3DN_SimpleFPSController playerController;
        public string PlayerName => SaveManager.instance.playerConfig.playerName;

        public int inventoryVolume = 10;

        [Header("===游戏事件系统===")]
        [SerializeField] private PlayableDirector director;
        [SerializeField] private GameEvent[] gameEvents;

        [SerializeField] private bool currentEventFinished;
        public bool CurrentEventFinished => currentEventFinished;

        [SerializeField] private int _index;

        [Header("===场景相关===")]
        public SceneType CurrentScene;
        public SceneConfig sceneConfig;
        [Tooltip("到下一个场景的传送门")]
        public InteractPortal portal;

        //TODO: zhe li yao gai tian qi

        public WeatherType CurrentWeather { get; private set; } = WeatherType.Sunny;
        //[Header("===天气===")]
        IL3DN.IL3DN_Snow snow;
        GameObject snowParticles;

        private void Start()
        {

            if (snow != null && snowParticles != null) ChangeWeather(CurrentWeather);//TODO: this code is really a shit
            InitData();
            InitGameplay();
            InitTimeline();
        }

        private void InitTimeline()
        {
            if (_index == 0)
            {
                if (playerController != null)
                    playerController.UpdateCursorLock(true);

                if (director != null)
                {
                    AblePlayerInputPrior(false);
                    Debug.Log("TimeLine Play!");
                    //AblePlayerInput(false);

                    UIManager.instance.OpenPanel(UIConst.TimelinePanel);

                    director.Play();
                    director.stopped += DirectorExecute;
                }
                else
                {
                    Debug.Log("no TimeLine Play!");
                    //Invoke(nameof(DelayExecute), 1f);
                    //DelayExecute();
                    DirectorExecute(null);
                }
            }
            else
            {
                Debug.Log("GameManager进入了index !=0 的情况");
                UIManager.instance.OpenPanel(UIConst.BarPanel);
                if (_index < gameEvents.Length)
                {
                    GameEventExecute();
                }
            }
        }


        private void InitGameplay()
        {
            switch (CurrentScene)
            {
                case SceneType.Start:
                    int t = Random.Range(0, 2);
                    if (t == 0) sceneConfig.startSnow.Snow = true;

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;

                case SceneType.Gameplay1:
                    if (sceneConfig.bridgeCollider != null) sceneConfig.bridgeCollider.SetActive(true);
                    snow = sceneConfig.snow;
                    snowParticles = sceneConfig.snowParticles;
                    break;
                case SceneType.Gameplay2:
                    break;
                case SceneType.End:
                    break;
            }

            _index = 0;//注意一下
            foreach (GameEvent gameEvent in gameEvents)
            {
                if (gameEvent.eventExecutor == null) Debug.LogError("GameEvent中执行器空了");
                if (gameEvent.interactable == null) Debug.LogError("GameEvent中interactable空了");
                gameEvent.interactable.Interaction += OnInteractAction;
                gameEvent.interactable.gameObject.SetActive(false);
            }

        }
        private void InitData()
        {
            UIManager.instance.GetPackageTable().ClearPackageItem();
            //starmap zai interactStarMap zhong wan chen
        }

        private void OnSequenceFinishedAction(bool success)
        {
            currentEventFinished = success;
            gameEvents[_index].interactable.gameObject.SetActive(true);
        }

        private void OnInteractAction()
        {
            if (currentEventFinished)
            {
                Debug.Log(_index + "已经触发");
                ExecuteNextGameEvent();
            }

        }

        private void ExecuteNextGameEvent()
        {
            //gameEvents[_index].interactable.gameObject.SetActive(true);
            if (_index == gameEvents.Length - 1)
            {
                Debug.Log("GameEvents 执行完了");
                currentEventFinished = false;
                return;
            }
            _index++;

            GameEventExecute();
            currentEventFinished = false;
        }

        private void GameEventExecute()
        {
            if(gameEvents.Length == 0) return;

            if (_index != 0) gameEvents[_index - 1].interactable.gameObject.SetActive(false);
            gameEvents[_index].eventExecutor.Init(OnSequenceFinishedAction);
            gameEvents[_index].eventExecutor.Execute();

        }

        private void DirectorExecute(PlayableDirector playableDirector)
        {
            if (director  != null)
            {
                UIManager.instance.ClosePanel(UIConst.TimelinePanel);
            }
            //playerController.gameObject.SetActive(true);

            if (CurrentScene != SceneType.Start)
                UIManager.instance.OpenPanel(UIConst.BarPanel);
            if (playerController != null)
                AblePlayerInput(true);

            GameEventExecute();
            if (playableDirector != null) playableDirector.gameObject.SetActive(false);
        }

        public void StopTimeline()
        {
            director.Stop();
        }

        public static void CloseAirWallBridge() => instance.sceneConfig.bridgeCollider.SetActive(false);

        public void AblePlayerInput(bool isEnable)
        {
            if (playerController != null)
            {
                playerController.enableInput = isEnable;
            }
        }
        public void AblePlayerInputPrior(bool isEnable)
        {
            if (playerController != null)
            {
                playerController.enableInputPrior = isEnable;
            }
        }

        public void AblePlayerCursor(bool isEnable)
        {
            if (playerController != null)
            {
                playerController.UpdateCursorLock(!isEnable);
            }
        }

        public void ChangeWeather(WeatherType type)
        {
            CurrentWeather = type;
            switch (type)
            {
                case WeatherType.Snowy:
                    if (TimeManager.instance.currentTime == GameTime.Night)
                    {
                        EventManager.instance.TriggerEvent("WeatherChange", null, 0, 0);
                    }
                    snowParticles.SetActive(true);
                    snow.Snow = true;
                    sceneConfig.snowTerrain.SetActive(true);
                    break;
                case WeatherType.Sunny:
                    snowParticles.SetActive(false);
                    snow.Snow = false;
                    sceneConfig.snowTerrain.SetActive(false);
                    break;
            }
        }
    }



    [Serializable]
    public class GameEvent
    {
        public SequenceEventExecutor eventExecutor;
        public Interactable interactable;
    }

    public enum WeatherType
    {
        Sunny, Snowy
    }
    public enum SceneType
    {
        Start, End, Gameplay1, Gameplay1Again, Gameplay2, Gameplay3, Gameplay4, Test, Gameplay5
    }
}