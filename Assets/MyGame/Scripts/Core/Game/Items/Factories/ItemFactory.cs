using System.Collections.Generic;
using TD.Item;

public static class ItemFactory
{
    private static Dictionary<string, IItemFactory> itemFactories;

    static ItemFactory(){
        itemFactories = new Dictionary<string, IItemFactory>(){
            { "HealthPotion", new HealthPotionFactory() },
            { "FrozenPotion", new FrozenPotionFactory() }
        };
    }

    public static Item GenerateItem(string itemName) => itemFactories[itemName].GenerateItem();
}
