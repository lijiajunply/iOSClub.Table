using System.ComponentModel.DataAnnotations;

namespace iOSClub.Table.Data;

public class PermissionsModel
{
    [Key]
    [MaxLength(256)]
    public string UserId { get; set; }
    public string Name { get; set; }
    
    /// <summary>
    /// Founder : 创始人
    /// President : 社长,副社长,秘书长
    /// TechnologyMinister : 科技部部长/副部长
    /// PracticalMinister : 实践交流部部长/副部长
    /// PracticalMember : 实践交流部成员
    /// NewMediaMinister : 新媒体部部长/副部长
    /// NewMediaMember : 新媒体部成员
    /// Member : 普通成员
    /// </summary>
    public string Identity { get; set; } = "Member";
    
    public PermissionsModel(){}

    public PermissionsModel(LoginModel model,string identity = "Member")
    {
        UserId = model.Id;
        Name = model.Name;
        Identity = identity;
    }
}