using System.Reflection.Metadata.Ecma335;
using PVE.Entities.BaseEntity;
using PVE.Entities.Players;
using PVE.Entities.Zombies;
using PVE.Items;
using PVE.Items.Weapons;

namespace PVE;

public abstract class GamePlay
{
    private static Player _player;
    private static bool exit = false;
    private static readonly ZombieFactory ZombieFactory = new ZombieFactory();

    public static void InitializeGame()
    {
        InitializePlayer();
    }

    private static void InitializePlayer()
    {
        if (new DirectoryInfo(ISavable.GetSavePath).GetFiles().Length > 0)
        {
            Printer.PrintAskForSavefile();
            string answer = Utils.GetPlayerInput().Normalize();
            if (string.Equals(answer, "y", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(answer, "yes", StringComparison.OrdinalIgnoreCase))
            {
                _player = ChooseSavefile();
                return;
            }
        }
        _player = CreatePlayer();
    }

    private static Player ChooseSavefile()
    {
        Printer.PrintNumberedList(Directory.GetFiles(ISavable.GetSavePath).ToList());
        Utils.GetAllowedIndex()
    }
    
    public static Player CreatePlayer()
    {
        Printer.PrintNameInput();
        string name = string.Empty;
        while (string.IsNullOrEmpty(name))
        {
            name = Utils.GetPlayerInput();
        }
        bool isDeveloper = Cheat.CheckName(name);
        return Player.CreatePlayer(name, Entity.BaseHp, Entity.BaseDamage, Entity.BaseArmor, isDeveloper);
    }

    public static void StartGame()
    {
        while (_player.Hp > 0 && !exit)
        {
            Console.Clear();
            Printer.PrintChoices(_player.IsDeveloper);
            string input = Utils.GetPlayerInput();
            if(_player.IsDeveloper && input.StartsWith('/'))
            {
                _player.AddItemToInventory(Cheat.HandleCommand(input));
                Utils.WaitForInput();
            }
            else
            { 
                int.TryParse(input, out int actionChoise);
                HandleAction(actionChoise);
            }
        }
    }
    
    private static void HandleAction(int input)
    {
        switch (input)
        {
            case 1: StartCombat(); break;
            case 2: _player.Heal(); break;
            case 3: ShowInventory(); break;
            case 4: exit = true; break;
            default: return;
        }
    }
    
    private static void HandleInventoryAction(int input)
    {
        switch (input)
        {
            case 1: ChooseWeapon(); break;                       //Use Weapon
            case 2: Console.WriteLine("Not implemented"); break; //Use Potion
            case 3: Console.WriteLine("Not implemented"); break; //Use Armor
            default: return;                                     //Go Back
        }
    }

    private static void ShowInventory()
    {
        Console.Clear();
        Printer.PrintInventory(_player.GetInventory);
        Printer.PrintInventoryChoice();
        Printer.PrintInputRequest();
        int.TryParse(Utils.GetPlayerInput(), out int actionChoice);
        HandleInventoryAction(actionChoice);
    }

    private static void ChooseWeapon()
    {
        Console.Clear();
        IReadOnlyList<Item> weapons = _player.GetWeapons;
        Printer.PrintInventory(weapons);
        Weapon w = ChooseItem(weapons) as Weapon;
        _player.EquipWeapon(w);
        Utils.WaitForInput();
    }

    private static Item ChooseItem(IReadOnlyList<Item> items)
    {
        Printer.PrintInventory(items);
        int index = Utils.GetAllowedIndex(items.Count);
        return items[index];
    }

    private static void StartCombat()
    {
        CombatSystem.HandleCombat(_player, ZombieFactory.Create());
    }

    public static void SaveAndExit()
    {
        _player?.Serialize();
        #if DEBUG
            new ZombieLootPool().Serialize();
        #endif
    }
}