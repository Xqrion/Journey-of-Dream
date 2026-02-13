using System;
using System.Collections;
using MyGameSystem.Core;
using MyGameSystem.Manager;
using TMPro;
using MyUI.Dialogue;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace MyGameSystem.Scene
{
    public class SceneLoader : PersistentSingleton<SceneLoader>
    {

        public CanvasGroup transitionImage;
        public float fadeTime = 1f;
        public float fadeDuration = 3f;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TextMeshProUGUI tip;
        [SerializeField] private string[] sceneTips;
        private string _tip;
        [SerializeField] private CursorA cursorA;


        const string SCENE_GAMEPLAY = "TheIllustratedNature_Demo";
        // const string SCENE_GAMEPLAY_TWICE = "TheIllustratedNature_Demo2";
        const string SCENE_START = "Scene_Start";
        // const string SCENE_END = "Scene_End";
        const string SCENE_GAMEPLAY2 = "TheIllustratedNature_PineForest";
        const string SCENE_GAMEPLAY4 = "Scene_Gameplay4";
        const string SCENE_GAMEPLAY3 = "Scene_Gameplay3";

        private AsyncOperation gameplayLoadingOperation;

        IEnumerator LoadWithTransition(string sceneName)
        {
            gameplayLoadingOperation = SceneManager.LoadSceneAsync(sceneName);
            if (gameplayLoadingOperation != null)
            {
                gameplayLoadingOperation.allowSceneActivation = false;

                transitionImage.gameObject.SetActive(true);

                _tip = sceneTips[Random.Range(0, sceneTips.Length)];

                tip.SetText("小贴士：" + _tip);
                cursorA.OpenCursor();

                float v = 0f;

                while (v < 1f)
                {
                    v = Mathf.Clamp01(v + Time.unscaledDeltaTime / fadeTime);
                    transitionImage.alpha = v;

                    yield return null;
                }

                float timer = 0;

                while (timer < fadeDuration)
                {
                    timer += Time.unscaledDeltaTime;
                    yield return null;
                }

                yield return new WaitUntil(() => gameplayLoadingOperation.progress >= 0.9f);

                gameplayLoadingOperation.allowSceneActivation = true;
                Debug.Log("Scene successfully change!");

                while (v > 0f)
                {
                    v = Mathf.Clamp01(v - Time.unscaledDeltaTime / fadeTime);
                    transitionImage.alpha = v;

                    yield return null;
                }
            }

            transitionImage.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.anyKeyDown && gameplayLoadingOperation != null)
            {
                var tempTip = sceneTips[Random.Range(0, sceneTips.Length)];
                while (tempTip == _tip)
                {
                    if (sceneTips.Length == 1) break;
                    tempTip = sceneTips[Random.Range(0, sceneTips.Length)];
                }

                _tip = tempTip;
                tip.SetText("小贴士：" + _tip);
            }
        }


        public void LoadScene(SceneType scene, string transitionText = "场景加载 i n g...")
        {
            StopAllCoroutines();

            //UI behavior when scene changes
            UIManager.instance.ClosePanel(UIConst.MapNavigationPanel);
            UIManager.instance.ClosePanel(UIConst.BarPanel);
            UIManager.instance.CloseDialogueBox();

            switch (scene)
            {
                case SceneType.Gameplay1:
                    StartCoroutine(LoadWithTransition(SCENE_GAMEPLAY));
                    break;
                case SceneType.Gameplay2:
                    StartCoroutine(LoadWithTransition(SCENE_GAMEPLAY2));
                    break;
                case SceneType.Start:
                    StartCoroutine(LoadWithTransition(SCENE_START));
                    break;
                case SceneType.Gameplay4:
                    StartCoroutine(LoadWithTransition(SCENE_GAMEPLAY4));
                    break;
                case SceneType.Gameplay3:
                    StartCoroutine(LoadWithTransition(SCENE_GAMEPLAY3));
                    break;

            }

            text.SetText(transitionText);
        }


    }
}
