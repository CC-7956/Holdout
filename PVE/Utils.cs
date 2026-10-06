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
        var choice = 0;
        do
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
            if (int.TryParse(input, out choice)) ;
        } while (choice is < 1 or > 4);
        return  choice;
    }

    private static int GetActionChoice()
    {
        var choice = 0;
        do
        {
            Printer.PrintInputRequest();
            if (int.TryParse(GetPlayerInput(), out choice)) ;
        } while (choice is < 1 or > 4);
        return  choice;
    }

    public static void WaitForInput()
    {
        Console.ReadKey();
    }
}