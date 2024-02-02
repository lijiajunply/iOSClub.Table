using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iOSClub.Share.Data;

public class EventModel
{
    public string Title { get; set; }
    [Key]
    [Column(TypeName = "varchar(256)")]
    public string Link { get; set; }
    public string Cover { get; set; }
    public string Digest { get; set; }
}