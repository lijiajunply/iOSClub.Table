using System.Text;

namespace iOSClub.Table.Data;

public class SignModel
{
    public string UserName { get; set; }
    public string Id { get; set; }
    public string Academy { get; set; }
    public string PoliticalLandscape { get; set; }
    public string Gender { get; set; }
    public string ClassName { get; set; }
    public string PhoneNum { get; set; }

    public override string ToString()
    {
        return $"{UserName},{Id},{Gender},{Academy},{PoliticalLandscape},{ClassName},{PhoneNum}";
    }

    public static string GetCsv(List<SignModel> models)
    {
        StringBuilder builder = new StringBuilder("姓名,学号,性别,学院,政治面貌,专业班级,电话号码");
        foreach (var model in models)
        {
            builder.Append("\n"+model);
        }
        return builder.ToString();
    }
}