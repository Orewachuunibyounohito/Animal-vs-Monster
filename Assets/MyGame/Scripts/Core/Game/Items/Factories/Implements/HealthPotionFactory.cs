using TD.Item;

public class HealthPotionFactory : IItemFactory
{
    public Item GenerateItem() => new HealthPotion();
}
