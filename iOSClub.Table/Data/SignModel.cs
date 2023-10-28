using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using AntDesign;

namespace iOSClub.Table.Data;

public class SignModel
{
    
    public string UserName { get; set; }
    
    [Key]
    [Column(TypeName = "varchar(256)")]
    public string UserId { get; set; }
    public string Academy { get; set; }
    public string PoliticalLandscape { get; set; }
    public string Gender { get; set; }
    public string ClassName { get; set; }
    public string PhoneNum { get; set; }

    public override string ToString()
    {
        return $"{UserName},{UserId},{Gender},{Academy},{PoliticalLandscape},{ClassName},{PhoneNum}";
    }

    public static string GetCsv(List<SignModel> models)
    {
        StringBuilder builder = new StringBuilder("姓名,学号,性别,学院,政治面貌,专业班级,电话号码");
        foreach (var model in models)
        {
            builder.Append("\n" + model);
        }

        return builder.ToString();
    }

    #region Table

    public static readonly string[] Academies =
    {
        "信息与控制工程学院",
        "理学院",
        "机电学院",
        "建筑学院",
        "机电工程学院",
        "管理学院",
        "土木工程学院",
        "环境与市政工程学院",
        "建筑设备科学与工程学院",
        "材料科学与工程学院",
        "冶金工程学院",
        "资源工程学院",
        "文学院",
        "艺术学院",
        "马克思主义学院",
        "公共管理学院",
        "化学与化工学院",
        "体育学院",
        "安德学院",
        "未来技术学院"
    };

    public static readonly string[] PoliticalLandscapes =
    {
        "群众",
        "共青团员",
        "中共党员",
        "中共预备党员"
    };

    public static readonly string[] Genders = { "男", "女" };

    public static FormValidationRule[] UserNameRules => new FormValidationRule[]
    {
        new() { Pattern = @"^[\u4E00-\u9FA5A-Za-z\s]+(·[\u4E00-\u9FA5A-Za-z]+)*$", Message = "姓名出错!", Required = true }
    };

    public static FormValidationRule[] IdRules => new FormValidationRule[]
        { new() { Len = 10, Message = "学号出错!", Pattern = "(19|20|21|22|23)([0-9]{8})", Required = true } };

    public static FormValidationRule[] PhoneNumRules => new FormValidationRule[]
        { new() { Len = 11, Pattern = "^1\\d{10}$", Message = "手机号不正确!", Required = true } };

    public static FormValidationRule[] ClassNameRules => new FormValidationRule[]
        { new() { Pattern = @"[\u4e00-\u9fa5]+[0-9]{4}", Message = "班级姓名出错!", Required = true } };

    public static FormValidationRule[] GenderRules => new FormValidationRule[]
        { new() { Required = true, Message = "性别没填!" } };

    public static FormValidationRule[] AcademyRules => new FormValidationRule[]
        { new() { Required = true, Message = "学院没填!" } };

    public static FormValidationRule[] PoliticalLandscapeRules => new FormValidationRule[]
        { new() { Required = true, Message = "政治面貌没填!" } };

    #endregion
}