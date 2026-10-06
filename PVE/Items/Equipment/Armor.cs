using PVE.Entities.BaseEntity;

namespace PVE.Items.Equipment;

public class Armor(string name, Rarity rarity, int defense, int toughness, Slot slot)
    : Item(name, rarity, slot)
{
    public int Defense { get; private set; } = defense;
    public int Toughness { get; private set; } = toughness;

}

