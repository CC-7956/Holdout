using PVE.Entities.BaseEntity;

namespace PVE.Items.Weapons;

public class Weapon(string name, Rarity rarity, int damage, Slot slot)
    : Item(name, rarity, slot)
{
    public int Damage { get; set; } = damage;
    public double CritChance { get; set; } = 0.5;

    public override string ToString()
    {
        return base.ToString() + $"[DMG: {Damage},  Crit rate: {CritChance}]";
    }
}