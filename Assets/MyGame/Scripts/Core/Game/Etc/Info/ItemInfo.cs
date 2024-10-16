using Sirenix.OdinInspector;

namespace TD.Info
{
    public class ItemInfo : SelectableComponenet, IInfoData
    {
        [ShowInInspector]
        public string Name { get; set; }
        [ShowInInspector]
        public string[] Detail { get; set; }

        public void SetInfo(NewItemData itemData){
            Name   = itemData.dataName;
            Detail = itemData.ToStringEx();
        }
    }
}
