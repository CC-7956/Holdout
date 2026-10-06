using PVE.Entities.BaseEntity;

namespace PVE.Items.Equipment;

public class Chestplate(string name, Rarity rarity, int defense, int toughness)
    : Armor(name, rarity, defense, toughness, Slot.Chest)
{
}