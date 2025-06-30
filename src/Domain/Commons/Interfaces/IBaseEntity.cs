namespace Domain.Commons.Interfaces;

public interface IBaseEntity
{
    public long Id { get; set; }
    
    public string CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
}