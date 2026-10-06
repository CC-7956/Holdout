using PVE.Entities.BaseEntity;
using PVE.Entities.Players;
using PVE.Items;

namespace PVE;

public abstract class CombatSystem
{
    public static void HandleCombat(Player player, Entity enemy)
    {
        while (player.Hp > 0 && enemy.Hp > 0)
        {
            Console.Clear();
            Printer.PrintHp(player);
            Printer.PrintHp(enemy);
            var dmg = Attack(player, enemy);
            Printer.PrintDmg(player, Math.Round(dmg, 2));
            if (player.Hp <= 0 || enemy.Hp <= 0) break;
            dmg = Attack(enemy, player);
            Printer.PrintDmg(enemy, Math.Round(dmg, 2));
            Thread.Sleep(500);
        }
        if(player.Hp <= 0)
            Printer.PrintDefeat(player, enemy);
        else
            Printer.PrintDefeat(enemy, player);
        
        if (enemy.Hp <= 0 && enemy is ILootable lootableEnemy)
        {
            Item i = lootableEnemy.DropLoot();
            player.AddItemToInventory(i);
        }
        Utils.WaitForInput();
    }
    
    private static double Attack(Entity attacker, Entity defender)
    {
        var effectiveDamage = (attacker.AttackDMG * CriticalMultiplier(attacker) * ArmorDamageReduction(defender));
        defender.ApplyDamage(effectiveDamage);
        return effectiveDamage;
    }

    private static double CriticalMultiplier(Entity e)
    {
        return Random.Shared.NextDouble() < e.CritRate ? 1.5 : 1;
    }

    private static double ArmorDamageReduction(Entity target) => Math.Max(0.3, 1 - target.Armor / 10d);
}
