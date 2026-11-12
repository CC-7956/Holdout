using System.Globalization;
using PVE.Entities.BaseEntity;
using PVE.Items;

namespace PVE;

public abstract class Printer
{
    private const int _padding = -20;
    public static void PrintNameInput()
    {
        Console.WriteLine("Enter a name for the player: ");
    }

    public static void PrintInputRequest()
    { 
        Console.WriteLine("Choose an action: ");
    }
    

    public static void PrintHp(Entity target)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(target.Name);
        Console.ResetColor();
        Console.Write(": ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(Math.Round(target.Hp, 2));
        Console.ResetColor();
        Console.WriteLine(" HP");
    }

    public static void PrintDefeat(Entity target, Entity killedBy)
    {
        Console.WriteLine($"{target.Name} was defeated by {killedBy.Name}");
    }

    public static void PrintDmg(Entity target, double dmg)
    {
        Console.WriteLine($"{target.Name} dealt {dmg} DMG");
    }

    public static void PrintChoices(bool isDeveloper)
    {
        Console.WriteLine($"{"1: Attack", _padding}{"2: Heal", _padding}");
        Console.WriteLine($"{"3: Inventory", _padding}{"4: Save & Exit", _padding}");
        if (isDeveloper)
        {
            Console.WriteLine($"Developer: '/' commands");
        }
    }

    public static void PrintLoot(Entity entity, Item item)
    {
        Console.Write($"{entity.Name} has dropped ");
        SetItemColor(item);
        Console.WriteLine(item.Name);
        Console.ResetColor();
    }
    
    public static void PrintCommandLoot(Item item)
    {
        Console.Write("A heavenly ray carries ");
        SetItemColor(item);
        Console.Write(item.Name);
        Console.ResetColor();
        Console.Write(" down to your Feet");
        Console.ResetColor();
    }

    public static void PrintInventory(IReadOnlyList<Item> playerInv)
    {
        Console.Clear();
        for(int line = 0; line < playerInv.Count; line++)
        {
            Console.Write($"{line + 1}: ");
            SetItemColor(playerInv[line]);
            Console.WriteLine(playerInv[line].Name);
            Console.ResetColor();
        }
    }
    
    public static void PrintNumberedList(List<string> items)
    {
        Console.Clear();
        for(int line = 0; line < items.Count; line++)
        {
            Console.Write($"{line + 1}: ");
            Console.WriteLine(items[line]);
        }
    }

    public static void PrintInventoryChoice()
    {
        Console.WriteLine();
        Console.WriteLine($"{"1: Equip Weapon", _padding}{"2: Use Potion", _padding}");
        Console.WriteLine($"{"3: Equip Armor", _padding}{"4: Main Menu", _padding}");
    }

    private static void SetItemColor(Item item)
    {
        Console.ForegroundColor = item.Rarity switch
        {
            Rarity.Developer => ConsoleColor.DarkRed,
            Rarity.Legendary => ConsoleColor.Yellow,
            Rarity.Epic => ConsoleColor.Magenta,
            Rarity.Rare => ConsoleColor.Blue,
            Rarity.Uncommon => ConsoleColor.Green,
            Rarity.Common => ConsoleColor.DarkGray,
            _ => Console.ForegroundColor
        };
    }

    public static void PrintHelp()
    {
        Console.WriteLine("/give | weapon  -> Gives a weapon to player");
        Console.WriteLine("      | armor  -> Not Implemented");
        Console.WriteLine("/? -> Help");
    }

    public static void PrintItemDoesNotExist(string itemName)
    {
        Console.WriteLine($"{itemName} does not exist in inventory");
    }

    public static void PrintItemEquipped(Item item)
    {
        Printer.SetItemColor(item);
        Console.Write($"{item.Name}");
        Console.ResetColor();
        Console.WriteLine(" equipped");
    }

    public static void PrintSaveFailed(string origin,string expectedPath, string json = "")
    {
        Console.WriteLine($"Failed to save {origin}");
        if (!string.IsNullOrEmpty(json))
        {
            Console.WriteLine($"Manuely save in {expectedPath}");
            Console.WriteLine(json);
        }
    }
    
    public static void PrintLoadFailed(string origin)
    {
        Console.WriteLine($"Failed to load {origin}");
        Console.WriteLine($"Check the file and restart the game");
    }

    public static void PrintAskForSavefile()
    {
        Console.WriteLine();
    }
}