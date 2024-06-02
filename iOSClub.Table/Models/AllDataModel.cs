﻿using iOSClub.Share.Data;

namespace iOSClub.Table.Models;

[Serializable]
public class AllDataModel
{
    public List<StudentModel> Students { get; init; } = [];
    public List<StaffModel> Staffs { get; init; } = [];
    public List<ArticleModel> Articles { get; init; } = [];
    public List<TaskModel> Tasks { get; init; } = [];
    public List<ProjectModel> Projects { get; init; } = [];
    public List<ResourceModel> Resources { get; init; } = [];
    public List<ToolModel> Tools { get; init; } = [];
}