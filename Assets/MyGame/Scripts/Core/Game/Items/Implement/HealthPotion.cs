using UnityEngine;

namespace TD.Item
{
    public class HealthPotion : Item
    {
        public HealthPotion(){
            var itemData = GameManager.Instance.Library.GetData<NewItemData>("HealthPotion");
            Name     = itemData.dataName;
            Capacity = itemData.Capacity;
            Icon     = itemData.image;
        }
        public HealthPotion(NewItemData itemData): base(itemData){}
        
        public override void Use(NewPlayer user){
            user.ChangeHp(10);
            base.Use(user);
        }
    }
}