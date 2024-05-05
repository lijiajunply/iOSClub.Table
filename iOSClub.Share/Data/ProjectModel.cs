using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iOSClub.Share.Data;

public class ProjectModel
{
    [Column(TypeName = "varchar(20)")] public string DepartmentName { get; set; } = "";
    [Column(TypeName = "varchar(20)")] public string Title { get; set; } = "";

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Column(TypeName = "varchar(512)")] public string Description { get; set; } = "";
    [Column(TypeName = "varchar(10)")] public string? StartTime { get; set; }
    [Column(TypeName = "varchar(10)")] public string? EndTime { get; set; }

    public void Update(ProjectModel model)
    {
        if (!string.IsNullOrEmpty(model.Title)) Title = model.Title;
        if (!string.IsNullOrEmpty(model.Description)) Description = model.Description;
        if (!string.IsNullOrEmpty(model.StartTime)) StartTime = model.StartTime;
        if (!string.IsNullOrEmpty(model.EndTime)) EndTime = model.EndTime;
    }

    public List<StaffModel> Staffs { get; init; } = [];
    public List<TaskModel> Tasks { get; init; } = [];
}