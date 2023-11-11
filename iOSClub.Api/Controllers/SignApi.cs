using iOSClub.Share.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iOSClub.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SignApi : ControllerBase
{
    private readonly JwtHelper _jwtHelper;
    private readonly SignContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SignApi(SignContext context, JwtHelper jwtHelper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _jwtHelper = jwtHelper;
        _httpContextAccessor = httpContextAccessor;
    }

    #region Visitor

    [TokenActionFilter]
    [Authorize]
    [HttpGet]
    public ActionResult<MemberModel> GetData()
    {
        var member = _httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        return member;
    }
    
    [HttpPost]
    public async Task<ActionResult<string>> SignUp(SignModel model)
    {
        if (DateTime.Today.Month != 10)
            return NotFound();

        if (_context.Students == null!)
        {
            return Problem("Entity set 'MemberContext.Students'  is null.");
        }

        _context.Students.Add(model);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (await MemberModelExists(model.UserId))
                return Conflict();

            throw;
        }

        //return MemberModel.AutoCopy<SignModel, MemberModel>(model);
        return _jwtHelper.GetMemberToken(MemberModel.AutoCopy<SignModel, MemberModel>(model));
    }


    [HttpPost]
    public async Task<ActionResult<string>> Login(LoginModel loginModel)
    {
        if (_context.Students == null!)
            return NotFound();

        var peo = await _context.Staffs.FirstOrDefaultAsync(x =>
            x.UserId == loginModel.Id && x.Name == loginModel.Name);

        if (peo != null) return _jwtHelper.GetMemberToken(new MemberModel(peo));
        var model =
            await _context.Students.FirstOrDefaultAsync(x =>
                x.UserId == loginModel.Id && x.UserName == loginModel.Name);

        if (model == null)
            return NotFound();
        return _jwtHelper.GetMemberToken(MemberModel.AutoCopy<SignModel, MemberModel>(model));
    }

    #endregion

    #region Ordinary Members

    // PUT: api/Member/5
    [TokenActionFilter]
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, MemberModel memberModel)
    {
        if (id != memberModel.UserId)
        {
            return BadRequest();
        }

        _context.Entry(memberModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await MemberModelExists(id))
                return NotFound();

            throw;
        }

        return NoContent();
    }

    [TokenActionFilter]
    [Authorize(Roles = "Founder, President, TechnologyMinister, PracticalMinister, NewMediaMinister")]
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

    [TokenActionFilter]
    [Authorize(Roles = "Founder, President, TechnologyMinister, PracticalMinister, NewMediaMinister")]
    [HttpGet]
    public async Task<List<MemberModel>> GetAllData()
    {
        var list = await _context.Students.ToListAsync();
        var newList = new List<MemberModel>();

        list.ForEach(Action);
        return newList;

        async void Action(SignModel x) => newList.Add(await FromSignToMember(x));
    }

    #endregion

    private async Task<bool> MemberModelExists(string id)
    {
        return await _context.Students.AnyAsync(e => e.UserId == id);
    }

    private async Task<MemberModel> FromSignToMember(SignModel model)
    {
        var member = MemberModel.AutoCopy<SignModel, MemberModel>(model);
        var m = await _context.Staffs.FirstOrDefaultAsync(x => x.UserId == model.UserId && x.Name == model.UserName);
        if (m != null) member.Identity = m.Identity;
        return member;
    }
}