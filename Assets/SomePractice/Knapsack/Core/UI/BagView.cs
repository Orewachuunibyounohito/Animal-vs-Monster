using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Knapsack.UI
{
    public class BagView : MonoBehaviour
    {
        public RectTransform Content{ get; private set; }

        [Header("New Image Settings")]
        [SerializeField][Indent]
        private float min = 50;
        [SerializeField][Indent]
        private float max = 200;

        private void Awake(){
            Content = transform.Find("View/Viewport/Content").GetComponent<RectTransform>();
        }

        private void Start()
        {
            InitialContentSize();
        }

        private void InitialContentSize()
        {
            var images      = Content.GetComponentsInChildren<Image>();
            var totalHeight = 0f;
            foreach (var image in images){ totalHeight += image.rectTransform.rect.height; }
            Content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, totalHeight);
        }

        #region Helper Function
        [ContextMenu("Add Image")]
        public void AddImageWithRangeHeight(){
            var height   = Random.Range(min, max);
            var newImage = new GameObject("newImage").AddComponent<Image>();
            newImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Content.rect.width);
            newImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            newImage.transform.SetParent(Content, false);
            newImage.transform.SetAsFirstSibling();
            
            var newHeight = Content.rect.height + newImage.rectTransform.rect.height;
            Content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
        }
        public void AddImage(string name, int height){
            Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            
            var newImage = new GameObject(name).AddComponent<Image>();
            newImage.color = color;
            newImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Content.rect.width);
            newImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            newImage.transform.SetParent(Content, false);
            newImage.transform.SetAsFirstSibling();
            
            var newHeight = Content.rect.height + newImage.rectTransform.rect.height;
            Content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
        }
        #endregion
    }
}
