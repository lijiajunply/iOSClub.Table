using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iOSClub.Share.Data;

public class ResourceModel
{
    [Key]
    public int Id { get; init; }

    [Column(TypeName = "varchar(20)")]
    public string Name { get; init; } = "";

    [Column(TypeName = "varchar(512)")]
    public string? Description { get; init; }

    [Column(TypeName = "varchar(50)")]
    public string? Tag { get; init; }
}