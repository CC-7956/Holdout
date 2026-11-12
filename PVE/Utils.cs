using PVE.Entities.BaseEntity;
using PVE.Entities.Players;
using PVE.Items;

namespace PVE;

public abstract class Utils
{
    public static EnemyArchetype GetEnemyArchetype()
    {
        return EnemyArchetype.All[Random.Shared.Next(EnemyArchetype.All.Count)];
    }
    
    public static string GetPlayerInput( )
    {
        var input = "";
        do
        {
            input = Console.ReadLine();
        }
        while (string.IsNullOrEmpty(input));
        return input;
    }

    public static int GetChoice(Player p)
    {
        if (p.IsDeveloper)
            return HandleDevCommand(p);
        
        return GetActionChoice();
    }

    private static int HandleDevCommand(Player p)
    {
        while(true)
        {
            Printer.PrintInputRequest();
            string input = GetPlayerInput();
            if (input.StartsWith('/'))
            {
                Item? i = Cheat.HandleCommand(input);
                if(i != null)
                    p.AddItemToInventory(i);
                continue;
            }
            
            if (int.TryParse(input, out int choice) && choice is < 1 or > 4)
                return choice;
        }
    }

    private static int GetActionChoice()
    {
        while(true)
        {
            Printer.PrintInputRequest();
            if (int.TryParse(GetPlayerInput(), out int choice) && choice is < 1 or > 4)
                return choice;
        }
    }

    public static void WaitForInput()
    {
        Console.ReadKey();
    }
    
    public static int GetAllowedIndex(int maxIndex)
    {
        if(maxIndex <= 0)
            throw new ArgumentException("maxIndex must be greater than 0");
        while (true)
        {
            Printer.PrintInputRequest();
            string input = Utils.GetPlayerInput();
            if (int.TryParse(input, out int index) && index > 0 && index <= maxIndex)
            {
                return index-1;
            }
        }
    }
}