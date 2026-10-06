using PVE.Entities.BaseEntity;
using PVE.Entities.Players;
using PVE.Entities.Zombies;
using PVE.Items;
using PVE.Items.Weapons;

namespace PVE;

public class GamePlay
{
    private static Player _player;
    private static bool exit = false;
    private static readonly ZombieFactory ZombieFactory = new ZombieFactory();
    
    public static void CreatePlayer()
    {
        Printer.PrintNameInput();
        string name = string.Empty;
        while (string.IsNullOrEmpty(name))
        {
            name = Utils.GetPlayerInput();
        }
        bool isDeveloper = Cheat.CheckName(name);
        _player = Player.CreatePlayer(name, Entity.BaseHp, Entity.BaseDamage, Entity.BaseArmor, isDeveloper);
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
            case 1: ChooseWeapon(); break;                       //Use Weaon
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
        int.TryParse(Utils.GetPlayerInput(), out int actionChoise);
        HandleInventoryAction(actionChoise);
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
        string input = string.Empty;
        int itemIndex = -1;
        do
        {
            input = Utils.GetPlayerInput();
        } while (!int.TryParse(input, out itemIndex) || (itemIndex -1 < 0 && itemIndex -1 >= items.Count));
        return items[itemIndex -1];
    }

    private static void StartCombat()
    {
        CombatSystem.HandleCombat(_player, ZombieFactory.Create());
    }

    public static void SaveAndExit()
    {
#if DEBUG
        _player.Serialize();
        new ZombieLootPool().Serialize();
#else
        _player.Serialize();
        new ZombieLootPool().Serialize();
#endif
    }
}