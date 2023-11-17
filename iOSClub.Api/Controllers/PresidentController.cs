using System.Text;
using iOSClub.Share.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace iOSClub.Api.Controllers;

[Authorize(Roles = "Founder, President, TechnologyMinister, PracticalMinister, NewMediaMinister")]
[TokenActionFilter]
[Route("api/[controller]/[action]")]
[ApiController]
public class PresidentController : ControllerBase
{
    private readonly SignContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PresidentController(SignContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    // GET: api/Member
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (_context.Students == null!)
            return NotFound();

        var memberModel = await _context.Students.FindAsync(id);

        if (memberModel == null)
            return NotFound();

        _context.Students.Remove(memberModel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<string>> GetAllData()
    {
        var list = await _context.Students.FromSql($"select * from Students").ToListAsync();

        var str = list.ToJson();
        return string.IsNullOrEmpty(str) ? GZipServer.CompressString("[]") : GZipServer.CompressString(str);
    }

    [HttpPost]
    public async Task<ActionResult<string>> GetMemberIdentity(LoginModel model)
    {
        var m = await _context.Staffs.FirstOrDefaultAsync(x => x.UserId == model.Id && x.Name == model.Name);
        return m == null ? "Member" : m.Identity;
    }

    [HttpPost]
    public async Task<ActionResult> Update([FromBody] SignModel model)
    {
        if (await _context.Students.AnyAsync())
            return NotFound();
        
        _context.Entry(model).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Students.AnyAsync(x => x.Equals(model)))
                return NotFound();
            throw;
        }

        return Ok();
    }
    
    [HttpPost]
    public async Task<ActionResult> UpdateIdentity([FromBody] PermissionsModel model)
    {
        
        if (await _context.Staffs.AnyAsync())
            return NotFound();
        
        _context.Entry(model).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Staffs.AnyAsync(x => x.Equals(model)))
                return NotFound();
            throw;
        }

        return Ok();
    }

    private async Task<MemberModel> FromSignToMember(SignModel model)
    {
        var member = MemberModel.AutoCopy<SignModel, MemberModel>(model);
        var m = await _context.Staffs.FirstOrDefaultAsync(x => x.UserId == model.UserId && x.Name == model.UserName);
        if (m != null) member.Identity = m.Identity;
        return member;
    }
}