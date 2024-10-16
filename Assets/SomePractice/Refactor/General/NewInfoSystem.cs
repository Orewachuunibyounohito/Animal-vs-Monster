using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Refactoring
{
    public class NewInfoSystem : BaseInfoSystem
    {
        private const int SPACING = 6;

        public NewInfoSystem(TextMeshProUGUI infoText) : base(infoText){}

        public float Speed { get; set; } = 8f;

        public override void DisplayInfo(){
            _infoText.StartCoroutine(PlayTask());
        }

        protected override IEnumerator PlayTask(){
            _infoText.text = "";
            var  content = _infoText.rectTransform.parent.GetComponent<RectTransform>();
            bool needScrolDown;
            
            _infoText.text += "\r";
            foreach(var line in _info){
                AutoSize();
                needScrolDown = _infoText.preferredHeight > content.parent.GetComponent<RectTransform>().rect.height;
                if (needScrolDown){
                    content.anchoredPosition += new Vector2(0, _infoText.fontSize + SPACING);
                }

                foreach (var letter in line)
                {
                    _infoText.text += letter;
                    if (!Skip) { yield return new WaitForSeconds(1 / Speed); }
                }
                _infoText.text += "\n\r";
            }
        }

        private void AutoSize(){
            float height = _infoText.preferredHeight;
            _infoText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            _infoText.rectTransform.parent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        // Can use Coroutine to print info
        public static List<string> DefaultArticle => 
            new List<string>{
                "AAAAAA",
                "BBBBBB",
                "CCCCCC",
                "DDDDDD",
                "EEEEEE",
                "FFFFFF",
            };

    }
}
