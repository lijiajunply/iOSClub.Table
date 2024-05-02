﻿using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using iOSClub.Share.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace iOSClub.Table.CentrePages.AdminPages;

public partial class Permissions
{
    [Inject] [NotNull] public IJSRuntime? JS { get; set; }
    [Inject] [NotNull] public IDbContextFactory<SignContext>? DbFactory { get; set; }

    private async Task UploadFiles(InputFileChangeEventArgs e)
    {
        var reader = new StreamReader(e.File.OpenReadStream());
        var result = await reader.ReadToEndAsync();
        reader.Dispose();
        if (string.IsNullOrEmpty(result)) return;
        var context = await DbFactory.CreateDbContextAsync();

        var list = JsonConvert.DeserializeObject<StaffsList>(result)!;
        await Update(context, list);

        await context.SaveChangesAsync();
        await Update(context);
        await context.DisposeAsync();
        StateHasChanged();
    }

    private List<SignModel> SearchResult { get; set; } = [];

    private string Department = "";

    private Task OpenTable(string department)
    {
        _visible = true;
        Department = department;
        return Task.CompletedTask;
    }

    private async Task Add(SignModel model)
    {
        var staff = new StaffModel() { Name = model.UserName, Identity = Department, UserId = model.UserId };
        await using var context = await DbFactory.CreateDbContextAsync();
        var isAdd = true;
        try
        {
            await context.Staffs.AddAsync(staff);
        }
        catch
        {
            isAdd = false;
        }

        if (isAdd) await context.SaveChangesAsync();
        switch (Department)
        {
            case "President":
                President.Add(staff);
                break;
            case "TechnologyMinister":
                TechnologyMinister.Add(staff);
                break;
            case "PracticalMinister":
                PracticalMinister.Add(staff);
                break;
            case "NewMediaMinister":
                NewMediaMinister.Add(staff);
                break;
            case "TechnologyMember":
                TechnologyMember.Add(staff);
                break;
            case "PracticalMember":
                PracticalMember.Add(staff);
                break;
            case "NewMediaMember":
                NewMediaMember.Add(staff);
                break;
        }

        StateHasChanged();
    }

    private async Task Search(string s)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        SearchResult = await context.Students.Where(x => x.UserName.StartsWith(s)).ToListAsync();
        StateHasChanged();
    }

    private bool _visible;

    private void HandleCancel()
    {
        SearchResult.Clear();
        _visible = false;
        Department = "";
    }

    private bool _operaVisible;
    private string _department = "";

    private void OpenOrClose(string department = "")
    {
        _department = department;
        _operaVisible = string.IsNullOrEmpty(department);
    }

    private List<StaffModel> President { get; set; } = new();
    private List<StaffModel> TechnologyMinister { get; set; } = new();
    private List<StaffModel> PracticalMinister { get; set; } = new();
    private List<StaffModel> NewMediaMinister { get; set; } = new();
    private List<StaffModel> TechnologyMember { get; set; } = new();
    private List<StaffModel> PracticalMember { get; set; } = new();
    private List<StaffModel> NewMediaMember { get; set; } = new();

    private async Task FileDownload()
    {
        var staffsList = new StaffsList()
        {
            President = President,
            TechnologyMinister = TechnologyMinister,
            PracticalMinister = PracticalMinister,
            NewMediaMinister = NewMediaMinister,
            TechnologyMember = TechnologyMember,
            PracticalMember = PracticalMember,
            NewMediaMember = NewMediaMember
        };
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        var jsonString = JsonSerializer.Serialize(staffsList, options);
        var data = Encoding.UTF8.GetBytes(jsonString);
        await Download("staff.json", data);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await using var context = await DbFactory.CreateDbContextAsync();
        await Update(context);
    }

    private async Task Update(SignContext context)
    {
        President = await context.Staffs.Where(x => x.Identity == "President").ToListAsync();
        TechnologyMinister = await context.Staffs.Where(x => x.Identity == "TechnologyMinister").ToListAsync();
        PracticalMinister = await context.Staffs.Where(x => x.Identity == "PracticalMinister").ToListAsync();
        NewMediaMinister = await context.Staffs.Where(x => x.Identity == "NewMediaMinister").ToListAsync();
        TechnologyMember = await context.Staffs.Where(x => x.Identity == "TechnologyMember").ToListAsync();
        PracticalMember = await context.Staffs.Where(x => x.Identity == "PracticalMember").ToListAsync();
        NewMediaMember = await context.Staffs.Where(x => x.Identity == "NewMediaMember").ToListAsync();
    }

    private async Task Update(SignContext context, StaffsList list)
    {
        context.Staffs.RemoveRange(President);
        context.Staffs.RemoveRange(TechnologyMinister);
        context.Staffs.RemoveRange(PracticalMinister);
        context.Staffs.RemoveRange(NewMediaMinister);
        await context.Staffs.AddRangeAsync(list.President);
        await context.Staffs.AddRangeAsync(list.TechnologyMinister);
        await context.Staffs.AddRangeAsync(list.PracticalMinister);
        await context.Staffs.AddRangeAsync(list.NewMediaMinister);
        await context.Staffs.AddRangeAsync(list.TechnologyMember);
        await context.Staffs.AddRangeAsync(list.PracticalMember);
        await context.Staffs.AddRangeAsync(list.NewMediaMember);
        await context.SaveChangesAsync();
    }

    private async Task Download(string fileName, byte[] data)
    {
        await JS.InvokeVoidAsync("jsSaveAsFile", fileName, Convert.ToBase64String(data));
    }

    [Serializable]
    class StaffsList
    {
        public List<StaffModel> President { get; init; } = [];
        public List<StaffModel> TechnologyMinister { get; init; } = [];
        public List<StaffModel> PracticalMinister { get; init; } = [];
        public List<StaffModel> NewMediaMinister { get; init; } = [];
        public List<StaffModel> TechnologyMember { get; init; } = [];
        public List<StaffModel> PracticalMember { get; init; } = [];
        public List<StaffModel> NewMediaMember { get; init; } = [];
    }

    private async Task Delete(StaffModel context, string department)
    {
        await using var dbContext = await DbFactory.CreateDbContextAsync();
        dbContext.Staffs.Remove(context);
        await dbContext.SaveChangesAsync();
        switch (department)
        {
            case "President":
                President.Remove(context);
                return;
            case "TechnologyMinister":
                TechnologyMinister.Remove(context);
                return;
            case "PracticalMinister":
                PracticalMinister.Remove(context);
                return;
            case "NewMediaMinister":
                NewMediaMinister.Remove(context);
                return;
            case "TechnologyMember":
                TechnologyMember.Remove(context);
                return;
            case "PracticalMember":
                PracticalMember.Remove(context);
                return;
            case "NewMediaMember":
                NewMediaMember.Remove(context);
                break;
        }
    }

    private async Task DeleteAll(string department)
    {
        await using var dbContext = await DbFactory.CreateDbContextAsync();
        switch (department)
        {
            case "President":
                dbContext.Staffs.RemoveRange(President);
                await dbContext.SaveChangesAsync();
                President.Clear();
                return;
            case "TechnologyMinister":
                dbContext.Staffs.RemoveRange(TechnologyMinister);
                await dbContext.SaveChangesAsync();
                TechnologyMinister.Clear();
                return;
            case "PracticalMinister":
                dbContext.Staffs.RemoveRange(PracticalMinister);
                await dbContext.SaveChangesAsync();
                PracticalMinister.Clear();
                return;
            case "NewMediaMinister":
                dbContext.Staffs.RemoveRange(NewMediaMinister);
                await dbContext.SaveChangesAsync();
                NewMediaMinister.Clear();
                return;
            case "TechnologyMember":
                dbContext.Staffs.RemoveRange(TechnologyMember);
                await dbContext.SaveChangesAsync();
                TechnologyMember.Clear();
                return;
            case "PracticalMember":
                dbContext.Staffs.RemoveRange(PracticalMember);
                await dbContext.SaveChangesAsync();
                PracticalMember.Clear();
                return;
            case "NewMediaMember":
                dbContext.Staffs.RemoveRange(NewMediaMember);
                await dbContext.SaveChangesAsync();
                NewMediaMember.Clear();
                break;
        }
    }
}