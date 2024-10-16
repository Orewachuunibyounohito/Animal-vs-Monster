namespace TD.Item
{
    public class FrozenPotion : Item
    {
        public FrozenPotion(){
            var itemData = GameManager.Instance.Library.GetData<NewItemData>("FrozenPotion");
            Name     = itemData.dataName;
            Capacity = itemData.Capacity;
            Icon     = itemData.image;
        }
        public FrozenPotion(NewItemData itemData): base(itemData){}
        
        public override void Use(NewPlayer user){
            base.Use(user);
        }
    }
}