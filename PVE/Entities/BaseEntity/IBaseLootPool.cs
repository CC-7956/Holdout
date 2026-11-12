using PVE.Items;

namespace PVE.Entities.BaseEntity;

public interface IBaseLootPool<out T> where T : IBaseLootPool<T>
{
    public T DeserializeLootPool();
}