using PVE.Items;

namespace PVE.Entities.BaseEntity;

public interface IBaseLootPool<out T> where T : Item
{
    public void DeserializeLootPool();
}