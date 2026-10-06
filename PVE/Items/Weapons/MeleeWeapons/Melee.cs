using PVE.Entities.BaseEntity;

namespace PVE.Items.Weapons.MeleeWeapons;

public class Melee(string name, Rarity rarity, int damage, Slot slot)
    : Weapon(name, rarity, damage, slot)
{
}