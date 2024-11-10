using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DeweyDecimalClassification.EfCore.Entities;

[PrimaryKey(nameof(Id))]
public class DeweyEntry
{
    public float Id { get; set; }
    [Required(AllowEmptyStrings = false)]
    [MaxLength(128)]
    public string Name { get; set; } = null!;
    
    public float? ParentId { get; set; }
    public DeweyEntry? Parent { get; set; }
    
    public ICollection<DeweyEntry> Children { get; set; }
}