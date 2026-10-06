using PVE.Entities.BaseEntity;

namespace PVE.Items.Equipment;

public class Helmet(string name, Rarity rarity, int defense, int toughness)
    : Armor(name, rarity, defense, toughness, Slot.Head)
{
}