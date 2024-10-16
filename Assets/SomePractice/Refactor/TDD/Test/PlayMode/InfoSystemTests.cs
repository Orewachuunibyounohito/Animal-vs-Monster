using NUnit.Framework;
using Refactoring;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoSystemTests
{
    [Test]
    public void UseDefaultArticleShowOnTextObject(){
        var infoPanel  = new GameObject("InfoPanel").AddComponent<Image>();
        var text       = new GameObject("Info").AddComponent<TextMeshProUGUI>();
        text.rectTransform.SetParent(infoPanel.rectTransform);
        var infoSystem = new NewInfoSystem(text);

        infoSystem.Add(NewInfoSystem.DefaultArticle);
        infoSystem.DisplayInfo();

        var actual   = text.text;
        var expected = "AAAAAA\nBBBBBB\nCCCCCC\nDDDDDD\nEEEEEE\nFFFFFF\n";

        Assert.AreEqual(expected, actual);
    }
}
