namespace Domain.Entities;

public abstract class BaseEntity
{
    public EntityStatus Status { get; protected set; }

    public void Activate()
    {
        Status = EntityStatus.Active;
    }

    public void Delete()
    {
        Status = EntityStatus.Deleted;
    }
}