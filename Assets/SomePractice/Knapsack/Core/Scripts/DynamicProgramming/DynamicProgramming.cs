using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knapsack.Algorithm
{
    public class DynamicProgramming
    {
        public int[][]    solutionTable;
        public Bag        bestBag;
        public List<Item> items;

        public DynamicProgramming(List<Item> items, int maxWeight){
            this.items = items;
            bestBag    = new Bag(maxWeight);
            solutionTable = new int[items.Count + 1][];
            for(int lineIndex = 0; lineIndex < solutionTable.Length; lineIndex++){
                solutionTable[lineIndex] = new int[maxWeight + 1];
                for(int slotIndex = 0; slotIndex < solutionTable[lineIndex].Length; slotIndex++){
                    solutionTable[lineIndex][slotIndex] = 0;
                }
            }
        }

        public Bag SolveKnapsack(){
            BuildSolutionTable();
            FindOutBestByTable();
            return bestBag;
        }

        private void BuildSolutionTable(){
            Item currentItem;
            int  remainingWeight;
            int  currentValue, newValue;
            for(int index = 1; index < solutionTable.Length; index++){
                for(int maxWeight = 1; maxWeight < solutionTable[index].Length; maxWeight++){
                    currentValue = solutionTable[index - 1][maxWeight];
                    currentItem  = items[index - 1];
                    if(currentItem.weight <= maxWeight){
                        remainingWeight = maxWeight - currentItem.weight;
                        newValue        = solutionTable[index - 1][remainingWeight] + currentItem.value;
                        solutionTable[index][maxWeight] = newValue > currentValue ? newValue : currentValue;
                    }
                }
            }
        }
        private void FindOutBestByTable(){
            Item currentItem;
            for(int index = solutionTable.Length - 1, remainingWeight = bestBag.MaxWeight; index >= 1; index--){
                currentItem = items[index-1];
                if(solutionTable[index - 1][remainingWeight] < solutionTable[index][remainingWeight]){
                    remainingWeight -= currentItem.weight;
                    bestBag.PutItemIn(currentItem);
                }
            }
        }
    }
}
