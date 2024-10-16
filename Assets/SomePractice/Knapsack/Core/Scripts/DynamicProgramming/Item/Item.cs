using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knapsack
{
    public class Item
    {
        public int weight;
        public int value;

        public Item(int weight, int value){
            this.weight = weight;
            this.value  = value;
        }

        public override string ToString(){
            return $"[weigth={weight}, value={value}]";
        }
    }
}
