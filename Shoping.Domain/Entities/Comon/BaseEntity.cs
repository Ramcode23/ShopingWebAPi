using System.ComponentModel.DataAnnotations;

namespace Shoping.Domain;
public class BaseEntity
{
      [Key]
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedByAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? CanceledAt { get; set; }
    public string? CanceledBy { get; set; }
    public bool IsCanceled { get; set; }
}
