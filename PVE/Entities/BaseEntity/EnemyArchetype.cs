namespace PVE.Entities.BaseEntity;

public class EnemyArchetype(string name, double healthMultiplier, double damageMultiplier, double armorMultiplier)
{
    public static readonly EnemyArchetype Normal = new("Normal", 1, 1, 0);
    private static readonly EnemyArchetype Strong = new("Strong", 0.8, 2, 0);
    private static readonly EnemyArchetype Armored = new("Armored", 1, 0.75, 1.5);
    private static readonly EnemyArchetype Tough = new("Tough", 2, 0.5, 0);

    public string Name { get; private set; } = name;
    public double HealthMultiplier { get; private set; } = healthMultiplier;
    public double DamageMultiplier { get; private set; } = damageMultiplier;
    public double ArmorMultiplier { get; private set; } =  armorMultiplier;
    public static readonly IReadOnlyList<EnemyArchetype> All = [Normal, Strong, Armored, Tough];



}


