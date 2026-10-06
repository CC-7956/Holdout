using System.Text.Json;
using PVE.Entities.BaseEntity;
using PVE.Items;
using PVE.Items.Equipment;
using PVE.Items.Weapons;

namespace PVE.Entities.Zombies;

public class ZombieLootPool : IBaseLootPool<Item>, ISaveable
{
    public IReadOnlyCollection<Item> LootPool { get; } =
    [
    // Common
    new Item("Rotten Flesh", Rarity.Common),
    new Item("Bone Fragment", Rarity.Common),
    new Item("Tattered Cloth", Rarity.Common),
    new Item("Broken Arrow", Rarity.Common),
    new Item("Old Leather", Rarity.Common),
    new Item("Rusty Shard", Rarity.Common),
    new Item("Cracked Tooth", Rarity.Common),
    new Item("Infected Tissue", Rarity.Common),
    new Item("Deadwood Branch", Rarity.Common),
    new Item("Small Health Potion", Rarity.Common),
    new Armor("Tattered Hood", Rarity.Common, 1, 0, Slot.Head),
    new Armor("Tattered Tunic", Rarity.Common, 3, 0, Slot.Chest),
    new Armor("Tattered Trousers", Rarity.Common, 2, 0, Slot.Legs),
    new Armor("Tattered Footwraps", Rarity.Common, 1, 0, Slot.Feet),

    // Uncommon
    new Weapon("Iron Dagger", Rarity.Uncommon, 12, Slot.Offhand),
    new Weapon("Throwing Knife", Rarity.Uncommon, 10, Slot.Offhand),
    new Weapon("Wooden Bow", Rarity.Uncommon, 14, Slot.Hand),
    new Item("Zombie Banner", Rarity.Uncommon, Slot.Back),
    new Item("Bloodied Bandage", Rarity.Uncommon),
    new Weapon("Cursed Bone", Rarity.Uncommon, 18, Slot.Hand),
    new Item("Rotten Charm", Rarity.Uncommon, Slot.Neck),
    new Item("Lesser Healing Potion", Rarity.Uncommon),
    new Item("Hunter's Quiver", Rarity.Uncommon, Slot.Back),
    new Item("Lost Adventurer's Map", Rarity.Uncommon),
    new Armor("Scavenger's Cap", Rarity.Uncommon, 2, 1, Slot.Head),
    new Armor("Scavenger's Vest", Rarity.Uncommon, 6, 1, Slot.Chest),
    new Armor("Scavenger's Pants", Rarity.Uncommon, 4, 1, Slot.Legs),
    new Armor("Scavenger's Shoes", Rarity.Uncommon, 2, 1, Slot.Feet),

    // Rare
    new Weapon("Bloodfang Dagger", Rarity.Rare, 38, Slot.Offhand),
    new Weapon("Graveyard Blade", Rarity.Rare, 55, Slot.Hand),
    new Weapon("Bone Reaper", Rarity.Rare, 60, Slot.Hand),
    new Weapon("Fleshbound Tome", Rarity.Rare, 48, Slot.Hand),
    new Weapon("Shadowfang", Rarity.Rare, 65, Slot.Hand),
    new Weapon("Vilethorn Fragment", Rarity.Rare, 45, Slot.Hand),
    new Item("Corrupted Essence", Rarity.Rare),
    new Item("Soul Lantern", Rarity.Rare, Slot.Offhand),
    new Item("Deathcap Mushroom", Rarity.Rare),
    new Armor("Gravewarden's Helm", Rarity.Rare, 5, 2, Slot.Head),
    new Armor("Gravewarden's Cuirass", Rarity.Rare, 15, 2, Slot.Chest),
    new Armor("Gravewarden's Greaves", Rarity.Rare, 10, 2, Slot.Legs),
    new Armor("Gravewarden's Sabatons", Rarity.Rare, 5, 2, Slot.Feet),

    // Epic
    new Weapon("Soulrender", Rarity.Epic, 120, Slot.Hand),
    new Weapon("Night's Edge", Rarity.Epic, 135, Slot.Hand),
    new Weapon("Bloodletter", Rarity.Epic, 145, Slot.Hand), 
    new Weapon("Gravekeeper's Scythe", Rarity.Epic, 160, Slot.Hand),
    new Weapon("Abyssal Cleaver", Rarity.Epic, 175, Slot.Hand),
    new Weapon("Hellforged Greatsword", Rarity.Epic, 190, Slot.Hand),
    new Weapon("Voidpiercer", Rarity.Epic, 165, Slot.Hand),
    new Weapon("Reaper's Fang", Rarity.Epic, 150, Slot.Hand),
    new Weapon("Darkmatter Staff", Rarity.Epic, 180, Slot.Hand),
    new Weapon("Phantom Bow", Rarity.Epic, 170, Slot.Hand),
    new Armor("Hellforged Visor", Rarity.Epic, 15, 4, Slot.Head),
    new Armor("Hellforged Breastplate", Rarity.Epic, 45, 4, Slot.Chest),
    new Armor("Hellforged Legplates", Rarity.Epic, 30, 4, Slot.Legs),
    new Armor("Hellforged Treads", Rarity.Epic, 15, 4, Slot.Feet),

    // Legendary
    new Weapon("Worldbreaker", Rarity.Legendary, 350, Slot.Hand),
    new Weapon("The Last Judgment", Rarity.Legendary, 400, Slot.Hand),
    new Weapon("Heaven's Wrath", Rarity.Legendary,420, Slot.Hand),
    new Weapon("Hell's Verdict", Rarity.Legendary, 430, Slot.Hand),
    new Weapon("Oblivion", Rarity.Legendary, 500, Slot.Hand),
    new Weapon("Eclipse", Rarity.Legendary, 460, Slot.Hand),
    new Weapon("The Black Sun", Rarity.Legendary, 520, Slot.Hand),
    new Weapon("Soul of the Fallen Star", Rarity.Legendary, 480, Slot.Hand),
    new Weapon("Dawnbringer", Rarity.Legendary, 500, Slot.Hand),
    new Weapon("The Devourer", Rarity.Legendary, 550, Slot.Hand),
    new Armor("Starfall Crown", Rarity.Legendary, 40, 8, Slot.Head),
    new Armor("Starfall Hauberk", Rarity.Legendary, 120, 8, Slot.Chest),
    new Armor("Starfall Legguards", Rarity.Legendary, 80, 8, Slot.Legs),
    new Armor("Starfall Striders", Rarity.Legendary, 40, 8, Slot.Feet),

    // Developer
    new Weapon("Genesis", Rarity.Developer, 9999, Slot.Hand),
    new Weapon("The Hand of Creation", Rarity.Developer, 9999, Slot.Hand),
    new Weapon("Armageddon", Rarity.Developer, 99999, Slot.Hand),
    new Weapon("The Final Authority", Rarity.Developer, -1, Slot.Hand),
    new Weapon("Null", Rarity.Developer, -1, Slot.Hand),
    new Armor("Creator's Halo", Rarity.Developer, 9999, 9999, Slot.Head),
    new Armor("Creator's Robe", Rarity.Developer, 9999, 9999, Slot.Chest),
    new Armor("Creator's Greaves", Rarity.Developer, 9999, 9999, Slot.Legs),
    new Armor("Creator's Sandals", Rarity.Developer, 9999, 9999, Slot.Feet)
];


    public void DeserializeLootPool()
    {
        throw new NotImplementedException();
    }

    public void Serialize()
    {
        string savePath = Path.Combine(ISaveable.GetSavePath, $"{GetType().Name}.json");
        
        string json = JsonSerializer.Serialize(this, ISaveable.GetJsonOptions);
        File.WriteAllText(savePath, json);
    }
}