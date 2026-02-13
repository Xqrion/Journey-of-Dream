using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace MyUI.Dialogue
{
    public class AdvancedTextPreprocessor : ITextPreprocessor
    {
        public Dictionary<int, float> intervalDictionary = new();

        public string PreprocessText(string text)
        {
            intervalDictionary.Clear();

            string processingText = text;
            string pattern = "<.*?>";
            Match match = Regex.Match(processingText, pattern);
            while (match.Success)
            {
                string label = match.Value.Substring(1, match.Length - 2);

                if (float.TryParse(label, out float result))
                {
                    intervalDictionary[match.Index - 1] = result;
                }

                processingText = processingText.Remove(match.Index, match.Length);
                if (Regex.IsMatch(label, "^sprite=.*"))
                {
                    processingText = processingText.Insert(match.Index, "*");
                }

                match = Regex.Match(processingText, pattern);
            }

            processingText = text;
            pattern = @"<(\d+)(\.\d+)?>";
            processingText = Regex.Replace(processingText, pattern, "");

            return processingText;
        }
    }

    public class AdvancedText : TextMeshProUGUI
    {
        public enum DisplayType
        {
            Default,
            Fading,
            Typing
        }

        private Widget _widget;

        protected override void Awake()
        {
            base.Awake();
            _widget = GetComponent<Widget>();
        }

        private float _defaultInterval = 0.1f;
        private readonly WaitForSecondsRealtime defaultInterval;

        private AdvancedTextPreprocessor SelfPreprocessor => (AdvancedTextPreprocessor)textPreprocessor;
        public Action onFinished;
        private Coroutine _typingCoroutine;

        public AdvancedText()
        {
            textPreprocessor = new AdvancedTextPreprocessor();
            defaultInterval = new WaitForSecondsRealtime(_defaultInterval);
        }

        public void Initialize()
        {
            SetText("");
        }

        public void Disappear(float duration = 0.2f)
        {
            _widget.Fade(0, duration, null);
        }

        public void QuickShowRemaining()
        {
            if (_typingCoroutine != null)
            {
                StopCoroutine(_typingCoroutine);

                for (; _typingIndex < m_characterCount; _typingIndex++)
                {
                    StartCoroutine(FadeInCharacter(_typingIndex));
                }
            }
            onFinished.Invoke();
        }

        public IEnumerator SetText(string content, DisplayType type, float duration = 0.2f, float defaultTime = 0.1f)
        {
            _defaultInterval = defaultTime;

            if (_typingCoroutine != null)
            {
                StopCoroutine(_typingCoroutine);
            }

            SetText(content);
            yield return null;

            switch (type)
            {
                case DisplayType.Default:
                    _widget.RenderOpacity = 1;
                    onFinished?.Invoke();
                    break;
                case DisplayType.Fading:
                    _widget.Fade(1, duration, onFinished);
                    break;
                case DisplayType.Typing:
                    _widget.Fade(1, duration, null);
                    _typingCoroutine = StartCoroutine(Typing());
                    break;
            }

        }

        private int _typingIndex;
        IEnumerator Typing()
        {
            ForceMeshUpdate();
            for (int i = 0; i < m_characterCount; i++)
            {
                SetSingleCharacterAlpha(i, 0);
            }

            _typingIndex = 0;
            while (_typingIndex < m_characterCount)
            {
                if (textInfo.characterInfo[_typingIndex].isVisible)
                {
                    StartCoroutine(FadeInCharacter(_typingIndex));
                }

                if (SelfPreprocessor.intervalDictionary.TryGetValue(_typingIndex, out float result))
                {
                    yield return new WaitForSecondsRealtime(result);
                }
                else
                {
                    yield return new WaitForSecondsRealtime(_defaultInterval);
                }

                _typingIndex++;
            }

            onFinished.Invoke();
        }

        private void SetSingleCharacterAlpha(int index, byte newAlpha)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[index];
            int matIndex = charInfo.materialReferenceIndex;
            int vertIndex = charInfo.vertexIndex;
            for (int i = 0; i < 4; i++)
            {
                textInfo.meshInfo[matIndex].colors32[vertIndex + i].a = newAlpha;
            }
            UpdateVertexData();
        }

        IEnumerator FadeInCharacter(int index, float duration = 0.5f)
        {
            if (duration <= 0)
            {
                SetSingleCharacterAlpha(index, 255);
            }
            else
            {
                float timer = 0;
                while (timer < duration)
                {
                    timer = Mathf.Min(duration, timer + Time.unscaledDeltaTime);
                    SetSingleCharacterAlpha(index, (byte)(255 * timer / duration));
                    yield return null;
                }
            }
        }
    }
}