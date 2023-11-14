using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iOSClub.Share.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public async Task<string> GetAllData()
    {
        var list = await _context.Students.ToListAsync();
        var newList = new List<MemberModel>();

        list.ForEach(Action);
        var str = newList.ToJson();
        return string.IsNullOrEmpty(str) ? "{}" : Compress(str);

        async void Action(SignModel x) => newList.Add(await FromSignToMember(x));
    }

    [HttpPost]
    public async Task<ActionResult> Update([FromBody] MemberModel memberModel)
    {
        if (await _context.Students.AnyAsync())
            return NotFound();
        
        var model = (SignModel)memberModel;
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
    
    /// <summary>
    /// 压缩Json字符串
    /// </summary>
    /// <param name="json">需要压缩的json串</param>
    /// <returns></returns>
    private static string Compress(string json)
    {
        var sb = new StringBuilder();
        using (var reader = new StringReader(json))
        {
            int ch;
            var lastCh = -1;
            var isQuoteStart = false;
            while ((ch = reader.Read()) > -1)
            {
                if ((char)lastCh != '\\' && (char)ch == '\"')
                    isQuoteStart = !isQuoteStart;
                if (!char.IsWhiteSpace((char)ch) || isQuoteStart)
                    sb.Append((char)ch);
                lastCh = ch;
            }
        }
        return sb.ToString();
    }


}