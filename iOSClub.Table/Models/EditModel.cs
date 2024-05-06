﻿using iOSClub.Share.Data;

namespace iOSClub.Table.Models;

public class TaskEditModel
{
    public Guid  Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime StartTime { get; set; } = DateTime.Today;
    public DateTime EndTime { get; set; } = DateTime.Today;

    public static TaskEditModel FromTask(TaskModel project)
        => new()
        {
            Title = project.Title,
            StartTime = DateTime.TryParse(project.StartTime, out var startTime) ? startTime : DateTime.Today,
            EndTime = DateTime.TryParse(project.EndTime, out var endTime) ? endTime : DateTime.Today.AddDays(7),
            Description = project.Description,
            Id = project.Id
        };

    public TaskModel ToTask()
        => new()
        {
            Title = Title,
            StartTime = StartTime.ToString("yyyy年MM月dd日"),
            EndTime = EndTime.ToString("yyyy年MM月dd日"),
            Description = Description,
            Id = Id
        };
}

public class ProjectEditModel
{
    public string Title { get; set; } = "";
    public string DepartmentName { get; set; } = "";
    public DateTime StartTime { get; set; } = DateTime.Today;
    public DateTime EndTime { get; set; } = DateTime.Today;
    public string Description { get; set; } = "";

    public static ProjectEditModel FromProject(ProjectModel project)
        => new()
        {
            Title = project.Title,
            DepartmentName = project.DepartmentName,
            StartTime = DateTime.TryParse(project.StartTime, out var startTime) ? startTime : DateTime.Today,
            EndTime = DateTime.TryParse(project.EndTime, out var endTime) ? endTime : DateTime.Today.AddDays(7),
            Description = project.Description
        };

    public ProjectModel ToProject()
        => new()
        {
            Title = Title,
            DepartmentName = DepartmentName,
            StartTime = StartTime.ToString("yyyy年MM月dd日"),
            EndTime = EndTime.ToString("yyyy年MM月dd日"),
            Description = Description
        };
}