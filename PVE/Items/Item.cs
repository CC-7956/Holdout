using PVE.Entities.BaseEntity;

namespace PVE.Items;

public class Item(string name, Rarity rarity, Slot slot = Slot.None)
{
    public string Name { get; init; } = name.Normalize();
    public Rarity Rarity { get; init; } = rarity;
    public Slot Slot { get; init; } = slot;
    
    public override string ToString() => Name;
}