using System;
using System.Collections.Generic;
using Knapsack.Algorithm;
using Knapsack.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Knapsack
{

    public class KnapsackMain : MonoBehaviour
    {
        [Header("Bag Settings")]
        [Indent]
        public int maxWeight;

        [Header("Item Settings")]
        [Indent]
        public ItemSettings itemSettings;

        private int                multiplier = 5;

        private List<Item>         items;
        private DynamicProgramming myDynamic;
        private Bag                bestBag;

        [SerializeField]
        private BagView            bagView;

        private void Awake(){
            items = CreateItemsByRandom(itemSettings);
            ShowItems();
            myDynamic = new DynamicProgramming(items, maxWeight);
        }
        
        private void Start(){
            bestBag = myDynamic.SolveKnapsack();
            var id = 1;
            foreach(Item item in bestBag.Items){
                bagView.AddImage($"Item {id}", item.weight * multiplier);
                id++;
            }
            Debug.Log($"{bestBag}");
        }

        private void ShowItems(){
            string itemsInfo = $"\n\t";
            foreach(var item in items){
                itemsInfo += item + ",\n\t";
            }
            itemsInfo = itemsInfo.Remove(itemsInfo.LastIndexOf(','));
            Debug.Log(itemsInfo);
        }
        

        // -------------------------
        public List<Item> CreateItemsByRandom(ItemSettings itemSettings){
            var items = new List<Item>();
            int weight, value;
            for(int count = 0; count < itemSettings.itemCount; count++){
                weight = Random.Range(itemSettings.weightRange.min, itemSettings.weightRange.max + 1);
                value  = Random.Range(itemSettings.valueRange.min, itemSettings.valueRange.max + 1);
                items.Add(new Item(weight, value));
            }
            return items;
        }

        [Serializable]
        public class ItemSettings
        {
            public int   itemCount;
            public Range weightRange;
            public Range valueRange;
        }

        [Serializable]
        public struct Range
        {
            public int min, max;
        }
    }
}
