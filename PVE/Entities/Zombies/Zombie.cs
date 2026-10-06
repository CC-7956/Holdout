using PVE.Entities.BaseEntity;
using PVE.Items;

namespace PVE.Entities.Zombies;

public class Zombie(string name, double hp, double dmg, double armor) : Entity(name, hp, dmg, armor), ILootable
{
    private readonly ZombieLootPool _lootHandler = new ZombieLootPool();
    
    public Item DropLoot()
    {
        Rarity rarity = ILootable.GetRarity();
        var possibleDrops = _lootHandler.LootPool.Where(x => x.Rarity == rarity).ToList();
        var actualDrop = possibleDrops[Random.Shared.Next(possibleDrops.Count)];
        Printer.PrintLoot(this, actualDrop);
        return actualDrop;
    }
}
