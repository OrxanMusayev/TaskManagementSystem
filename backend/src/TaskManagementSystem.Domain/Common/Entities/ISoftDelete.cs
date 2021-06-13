namespace TaskManagementSystem.Domain.Common.Entities
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
