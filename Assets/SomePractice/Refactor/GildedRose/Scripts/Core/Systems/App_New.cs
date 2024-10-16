using System;
using System.Collections.Generic;
using Refactoring;

namespace GildedRose
{
    public class App_New
    {
        private const string AGED_BRIE        = "Aged Brie";
        private const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
        private const string SULFURAS         = "Sulfuras, Hand of Ragnaros";
        private const string CONJURED         = "Conjured Mana Cake";
        private const int    MAX_QUALITY = 50;
        IList<Item> Items = new List<Item>();
        public App_New(IList<Item> items)
        {
            foreach(var item in items){ AddItem(item); }
        }

        public void AddItem(Item item){
            if(item.Name == SULFURAS){ item.SellIn = Math.Max(item.SellIn, 0); }
            Items.Add(item);
        }

        public void UpdateQuality()
        {
            // for (var i = 0; i < Items.Count; i++)
            foreach(var item in Items)
            {
                int deltaQuality = 1;
                switch(item.Name){
                    case AGED_BRIE:
                        item.SellIn--;
                        deltaQuality = item.SellIn < 0? deltaQuality * 2 : deltaQuality;
                        item.Quality = Math.Clamp(item.Quality + deltaQuality, 0, MAX_QUALITY);
                        break;
                    case BACKSTAGE_PASSES:
                        item.SellIn--;
                        deltaQuality = item.SellIn < 0? -50 :
                                       item.SellIn <= 5? 3 :
                                       item.SellIn <= 10? 2 : deltaQuality;
                        item.Quality = Math.Clamp(item.Quality + deltaQuality, 0, MAX_QUALITY);
                        break;
                    case CONJURED:
                        item.SellIn--;
                        deltaQuality *= 2;
                        item.Quality = Math.Clamp(item.Quality - deltaQuality, 0, MAX_QUALITY);
                        break;
                    case SULFURAS:
                        break;
                    default:
                        item.SellIn--;
                        deltaQuality = item.SellIn < 0? deltaQuality * 2 : deltaQuality;
                        item.Quality = Math.Clamp(item.Quality - deltaQuality, 0, MAX_QUALITY);
                        break;
                }
            }
        }

        public void ShowProducts(){
            foreach(var item in Items){
                InfoSystem.Add($"{item.Name, -40}, {item.SellIn, -8}, {item.Quality, -8}");
                // InfoSystem.Add(item.ToString());
            }
        }

        public static IList<Item> DefaultItemList()
        {  
            return new List<Item>{
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = AGED_BRIE, SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = SULFURAS, SellIn = 0, Quality = 80},
                new Item {Name = SULFURAS, SellIn = -1, Quality = 80},
                new Item {Name = BACKSTAGE_PASSES, SellIn = 15, Quality = 20},
                new Item {Name = BACKSTAGE_PASSES, SellIn = 10, Quality = 49},
                new Item {Name = BACKSTAGE_PASSES, SellIn = 5, Quality = 49},
                new Item {Name = CONJURED, SellIn = 3, Quality = 6}
            };
        }
    }
}
