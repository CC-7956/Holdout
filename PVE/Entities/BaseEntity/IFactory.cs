namespace PVE.Entities.BaseEntity;

public interface IFactory<out T> where T : Entity
{
    public T Create();
}