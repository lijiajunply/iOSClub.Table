using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iOSClub.Share.Data;

public class TaskModel
{
    public ProjectModel Project { get; set; } = new();
    [Column(TypeName = "varchar(20)")] public string Title { get; set; } = "";
    [Column(TypeName = "varchar(200)")] public string Description { get; set; } = "";
    [Column(TypeName = "varchar(10)")] public string StartTime { get; set; } = "";
    [Column(TypeName = "varchar(10)")] public string EndTime { get; set; } = "";
    
    [Column(TypeName = "boolean")]public bool Status { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public List<StaffModel> Users { get; init; } = [];

    public void Update(TaskModel model)
    {
        if (!string.IsNullOrEmpty(model.Title)) Title = model.Title;
        if (!string.IsNullOrEmpty(model.Description)) Description = model.Description;
        if (!string.IsNullOrEmpty(model.StartTime)) StartTime = model.StartTime;
        if (!string.IsNullOrEmpty(model.EndTime)) EndTime = model.EndTime;
        Status = model.Status;
    }
}