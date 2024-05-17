using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iOSClub.Share.Data;

public class FileModel
{
    [Column(TypeName = "varchar(64)")]
    public string Path { get; set; } = "";
    [Column(TypeName = "varchar(64)")]
    public string Url { get; set; } = "";
    
    [Column(TypeName = "boolean")]
    public bool IsFolder { get; set; }
    
    [Key]
    public int Id { get; set; }
}