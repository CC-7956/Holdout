using System.Collections.ObjectModel;
using System.Security;
using System.Text.Json;
using PVE.Entities.BaseEntity;
using PVE.Exceptions;
using PVE.Items;
using PVE.Items.Equipment;
using PVE.Items.Weapons;

namespace PVE.Entities.Players;

public class Player : Entity, ISavable
{
    private const string FileName = nameof(Player);
    private Collection<Item> _inventory = [];
    private Weapon? Hand { get; set; }
    private Item? Head { get; set; } = null;
    private Item? Chest { get; set; } = null;
    private Item? Legs { get; set; } = null;
    private Item? Feet { get; set; } = null;
    public bool IsDeveloper { get; init; }

    private Player(string name, double hp, double dmg, double armor, bool isDeveloper) : base(name, hp, dmg, armor)
    {
        IsDeveloper = isDeveloper;
    }
    
    public static Player CreatePlayer(string name, double hp, double dmg, double armor, bool dev)
    {
        return new Player(name, hp, dmg, armor, dev);
    }

    public ReadOnlyCollection<Item> GetInventory =>  _inventory.AsReadOnly();
    public ReadOnlyCollection<Item> GetWeapons =>  GetInventory.Where(item => item is Weapon).ToList().AsReadOnly();
    public ReadOnlyCollection<Item> GetArmor =>  GetInventory.Where(item => item is Armor).ToList().AsReadOnly();
    public override double AttackDMG => BaseDmg + (Hand?.Damage ?? 0);
    public override double CritRate => CritChance + (Hand?.CritChance ?? 0);
    
    
    public void AddItemToInventory(Item? item)
    {
        if (item is not null)
        {
            _inventory.Add(item);
        }
    }
    
    public void EquipWeapon(Weapon weapon)
    {
        if(_inventory.FirstOrDefault(i => i == weapon) is not null)
        {
            Printer.PrintItemEquipped(weapon);
            Hand = weapon;
        }
        else
        {
            Printer.PrintItemDoesNotExist(nameof(weapon));
        }
    }

    public void Serialize()
    {
        
        string savePath = Path.Combine(ISavable.GetSavePath, $"{FileName}.json");
        string json = JsonSerializer.Serialize(this, ISavable.GetJsonOptions);
        try
        {
            File.WriteAllText(savePath, json);
        }
        catch
        {
            Printer.PrintSaveFailed(FileName, savePath, json);
            throw;
        }
    }
    
    public Player Deserialize()
    {
        string savePath = Path.Combine(ISavable.GetSavePath, $"{FileName}.json");
        string fileContent = File.ReadAllText(savePath);
        try
        {
            return JsonSerializer.Deserialize<Player>(fileContent, ISavable.GetJsonOptions) ??
                   throw new SaveFileCorruptedException($"{FileName} savefile was not able to be read or is empty", fileContent);
        }
        catch
        {
            Printer.PrintLoadFailed(FileName);
            throw;
        }
    }
}