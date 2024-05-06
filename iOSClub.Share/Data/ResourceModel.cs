using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iOSClub.Share.Data;

public class ResourceModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Column(TypeName = "varchar(20)")] public string Name { get; set; } = "";

    [Column(TypeName = "varchar(512)")] public string? Description { get; set; }

    [Column(TypeName = "varchar(50)")] public string? Tag { get; set; }
}