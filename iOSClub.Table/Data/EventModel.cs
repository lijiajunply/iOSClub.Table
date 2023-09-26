using Microsoft.AspNetCore.Components;

namespace iOSClub.Table.Data;

public class EventModel
{
    public RenderFragment Extra { get; set; }
    public string Description { get; set; }
    
    public string Url { get; set; }
    public string Title { get; set; }
}