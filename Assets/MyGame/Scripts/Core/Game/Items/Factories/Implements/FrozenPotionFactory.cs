using TD.Item;

public class FrozenPotionFactory : IItemFactory
{
    public Item GenerateItem() => new FrozenPotion();
}
