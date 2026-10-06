namespace PVE.Entities.BaseEntity;

public abstract class Entity
{
    public string Name { get; protected set; }
    public double Hp { get; protected set; }
    public double BaseDmg { get; protected set; }
    public double Armor { get; protected set; }
    protected readonly double MaxHp;
    public static readonly int BaseHp = 20;
    public static readonly int BaseArmor = 1;
    public static readonly int BaseDamage = 1;
    protected double CritChance { get; set; } = 0.05;
    
    public virtual double AttackDMG => BaseDmg;
    public virtual double CritRate => CritChance;
    
    
    internal Entity(string name, double hp, double dmg, double armor)
    { 
        Name = name;
        Hp = hp;
        MaxHp = hp;
        BaseDmg = dmg;
        Armor = armor;
    }

    

    internal bool ApplyDamage(double dmg)
    {
        Hp -= dmg;
        return Hp <= 0;
    }


    public void Heal()
    {
        this.Hp = this.Hp + this.Hp * 0.2 > this.MaxHp ? this.MaxHp : this.Hp + this.Hp * 0.2;
    }
}