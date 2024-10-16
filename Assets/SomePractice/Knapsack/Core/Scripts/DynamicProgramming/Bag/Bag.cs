using System.Collections.Generic;

namespace Knapsack
{
    public class Bag
    {
        public int        MaxWeight{ get; set; }
        public List<Item> Items{ get; set; }

        public int CurrentWeight{ get; set; }
        public int TotalValue{ get; set; }

        public Bag(int maxWeight){
            MaxWeight = maxWeight;
            Items     = new List<Item>();
            CurrentWeight = 0;
            TotalValue   = 0;
        }

        public bool PutItemIn(Item item){
            var newWeight = CurrentWeight + item.weight;
            if(newWeight > MaxWeight){ return false; }

            CurrentWeight = newWeight;
            TotalValue  += item.value;
            Items.Add(item);
            return true;
        }

        public override string ToString(){
            string result = $"Bag[\n\tMaxWeight={MaxWeight},\n\tCurrentWeight={CurrentWeight},\n\tTotalValue={TotalValue},\n\tItems=\n\t";
            foreach(var item in Items){
                result += $"{item},\n\t";
            }
            result = result.Remove(result.LastIndexOf(',')) + "]";
            return result;
        }
    }
}
