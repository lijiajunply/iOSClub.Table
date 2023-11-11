using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iOSClub.Share.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iOSClub.Api.Controllers;

[Authorize(Roles = "Founder, President, TechnologyMinister, PracticalMinister, NewMediaMinister")]
[TokenActionFilter]
[Route("api/[controller]/[action]")]
[ApiController]
public class PresidentController : ControllerBase
{
    private readonly SignContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PresidentController(SignContext context,IHttpContextAccessor httpContextAccessor)
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
    public async Task<List<MemberModel>> GetAllData()
    {
        var list = await _context.Students.ToListAsync();
        var newList = new List<MemberModel>();

        list.ForEach(Action);
        return newList;

        async void Action(SignModel x) => newList.Add(await FromSignToMember(x));
    }

    [HttpPost]
    public async Task<ActionResult> Update([FromBody] MemberModel model)
    {
        if (_context.Students == null!)
        {
            return NotFound();
        }
        
        _context.Entry(model).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            var a = await _context.Students.AnyAsync(x => x.Equals(model));
            if (!a)
                return NotFound();
            throw;
        }

        return NoContent();
    }

    private async Task<MemberModel> FromSignToMember(SignModel model)
    {
        var member = MemberModel.AutoCopy<SignModel, MemberModel>(model);
        var m = await _context.Staffs.FirstOrDefaultAsync(x => x.UserId == model.UserId && x.Name == model.UserName);
        if (m != null) member.Identity = m.Identity;
        return member;
    }

}