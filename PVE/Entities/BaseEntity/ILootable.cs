using PVE.Items;

namespace PVE.Entities.BaseEntity;

public interface ILootable
{
    public Item DropLoot();
    
    protected static Rarity GetRarity()
    {
        var roll = Random.Shared.NextDouble();

        return roll switch
        {
            >= 0.90 => Rarity.Legendary,
            >= 0.70 => Rarity.Epic,
            >= 0.50 => Rarity.Rare,
            >= 0.20 => Rarity.Uncommon,
            _ => Rarity.Common
        };
    }
}
