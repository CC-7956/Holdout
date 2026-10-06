using PVE.Entities.BaseEntity;

namespace PVE.Entities.Zombies;

public class ZombieFactory : IFactory<Zombie>
{
    private const string Name = "Zombie";

    public Zombie Create()
    {
        var type = Utils.GetEnemyArchetype();
        var name = type.Name == EnemyArchetype.Normal.Name ? Name : type.Name + " " + Name;
        var hp = Math.Round(Entity.BaseHp * type.HealthMultiplier,2);
        var dmg = Math.Round(Entity.BaseDamage * type.DamageMultiplier,2);
        var armor = Math.Round(Entity.BaseArmor *  type.ArmorMultiplier,2);

        return new Zombie(name, hp, dmg, armor);
    }
    
    
}