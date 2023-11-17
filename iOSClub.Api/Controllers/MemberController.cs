using iOSClub.Share.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iOSClub.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class MemberController : ControllerBase
{
    private readonly JwtHelper _jwtHelper;
    private readonly SignContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MemberController(SignContext context, JwtHelper jwtHelper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _jwtHelper = jwtHelper;
        _httpContextAccessor = httpContextAccessor;
    }

    #region Visitor

    [TokenActionFilter]
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<SignModel>> GetData()
    {
        var member = _httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var model =
            await _context.Students.FirstOrDefaultAsync(x =>
                x.UserId == member.UserId && x.UserName == member.UserName);
        if (model == null)
            return await _context.Staffs.AnyAsync(x => x.UserId == member.UserId || x.Name == member.UserName)
                ? member
                : NotFound();

        return model;
    }

    [HttpPost]
    public async Task<ActionResult<string>> SignUp(SignModel model)
    {
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
    public async Task<IActionResult> Update(string id, SignModel model)
    {
        if (id != model.UserId)
            return NotFound();

        _context.Entry(model).State = EntityState.Modified;

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

        return Ok();
    }

    #endregion

    private async Task<bool> MemberModelExists(string id)
    {
        return await _context.Students.AnyAsync(e => e.UserId == id);
    }
}