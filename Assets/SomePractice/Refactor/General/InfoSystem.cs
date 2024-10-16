using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Refactoring
{
    public static class InfoSystem
    {
        public static int Count => _info.Count;

        private static List<string> _info;
        private static TextMeshProUGUI _infoText;

        static InfoSystem() => _info = new List<string>();

        public static void SetTextUI(TextMeshProUGUI infoText) => _infoText = infoText;

        public static void Add(string line) => _info.Add(line);

        public static void DisplayInfo(bool toEnd){
            _infoText.text = "";
            foreach(var line in _info){
                _infoText.text += line + "\n";
            }
            
            var content = _infoText.rectTransform.parent.GetComponent<RectTransform>();
            var height  = _infoText.preferredHeight;
            content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

            if(toEnd){ content.anchoredPosition = new Vector2(0, height - (float)Camera.main.pixelHeight / 2 + 50); }
        }

        // Can use Coroutine to print info

    }
}
