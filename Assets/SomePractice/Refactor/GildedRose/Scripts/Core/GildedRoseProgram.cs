using System.Collections.Generic;
using Refactoring;
using UnityEngine;

namespace GildedRose
{
    public class GildedRoseProgram : MonoBehaviour
    {
        private const int MONTH_OF_DAY = 31;

        private void Start()
        {
            InfoSystem.SetTextUI(GameObject.Find("InfoText").GetComponent<TMPro.TextMeshProUGUI>());

            InfoSystem.Add("OMGHAI!");

            IList<Item> Items = App_New.DefaultItemList();
            
            var app = new App_New(Items);

            for (var i = 1; i <= MONTH_OF_DAY; i++)
            {
                InfoSystem.Add("-------- day " + i + " --------");
                InfoSystem.Add($"{"name", -40}, {"sellIn", -8}, {"quality", -8}");
                app.ShowProducts();
                InfoSystem.Add("");
                InfoSystem.DisplayInfo(false);
                app.UpdateQuality();
            }
        }
    }
}