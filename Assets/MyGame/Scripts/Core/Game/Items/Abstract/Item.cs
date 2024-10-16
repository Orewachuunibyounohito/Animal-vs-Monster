using System;
using UnityEngine;

namespace TD.Item
{
    public abstract class Item : IComparable<Item>
    {
        public string     Name{ get; protected set; }
        public ulong      Capacity{ get; protected set; }
        public Sprite     Icon{ get; protected set; }

        public delegate void ItemUsed();
        public ItemUsed Used;

        public Item(){}
        public Item(NewItemData itemData){
            Name     = itemData.dataName;
            Capacity = itemData.Capacity;
            Icon     = itemData.image;
        }

        public int CompareTo(Item other){
            int result = 0;
            var enumerator = Name.GetEnumerator();
            var otherEnumrator = other.Name.GetEnumerator();
            while(enumerator.MoveNext() && otherEnumrator.MoveNext()){
                result = enumerator.Current - otherEnumrator.Current;
            }
            if(enumerator.MoveNext())         { return 1; }
            else if(otherEnumrator.MoveNext()){ return -1; }
            
            return result;
        }

        public virtual void Use(NewPlayer user/* user, target */){
            Used?.Invoke();
        }
    }
}