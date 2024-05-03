﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace iOSClub.Share.Data;

public class StudentModel
{
    [Column(TypeName = "varchar(50)")] public string UserName { get; set; } = "";

    [Key]
    [Column(TypeName = "varchar(10)")]
    public string UserId { get; set; } = "";

    [Column(TypeName = "varchar(50)")] public string Academy { get; set; } = "";
    [Column(TypeName = "varchar(10)")] public string PoliticalLandscape { get; set; } = "";
    [Column(TypeName = "varchar(2)")] public string Gender { get; set; } = "";
    [Column(TypeName = "varchar(20)")] public string ClassName { get; set; } = "";
    [Column(TypeName = "varchar(11)")] public string PhoneNum { get; set; } = "";

    public override string ToString()
    {
        return $"{UserName},{UserId},{Gender},{Academy},{PoliticalLandscape},{ClassName},{PhoneNum}";
    }

    public StudentModel Standardization()
    {
        UserId = UserId.Replace(" ", "");
        return this;
    }

    public static string GetCsv(List<StudentModel> models)
    {
        var builder = new StringBuilder("姓名,学号,性别,学院,政治面貌,专业班级,电话号码");
        foreach (var model in models)
            builder.Append("\n" + model);

        return builder.ToString();
    }
}