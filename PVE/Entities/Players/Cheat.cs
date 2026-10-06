using PVE.Entities.BaseEntity;
using PVE.Items;
using PVE.Items.Weapons;

namespace PVE.Entities.Players;

public class Cheat
{
    private static readonly HashSet<string> DeveloperNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "IAmADeveloper",
        "IAmACheater",
        "IBrokeTheCode",
        "DeanTheBean9",
        "DTB9",
        "1DuraFishingRod",
        "ItsAllYours",
    };

    public static bool CheckName(string? name) =>
        name is not null && DeveloperNames.Contains(name);
    
    public static Item? HandleCommand(string input)
    {
        if (input.Contains('?'))
        {
            Printer.PrintHelp();
        }
        else if (input.Contains("give"))
        {
            switch(input.Split(' ')[1])
            {
                case "weapon":
                    Weapon w = new Weapon("Genesis", Rarity.Developer, 9999, Slot.None);
                    Printer.PrintCommandLoot(w);
                    return w;
                case "armor":
                    throw new NotImplementedException("Work in progress");
            }
        }
        return null;
    }
    
    
    
    
}