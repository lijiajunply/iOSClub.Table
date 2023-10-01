using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iOSClub.Table.Data;

namespace iOSClub.Table.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SignApi : ControllerBase
{
    private readonly SignContext _context;

    public SignApi(SignContext context)
    {
        _context = context;
    }

    // GET: api/SignApi
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SignModel>>> GetStudents()
    {
        if (_context.Students == null)
        {
            return NotFound();
        }

        return await _context.Students.ToListAsync();
    }

    // GET: api/SignApi/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SignModel>> GetSignModel(string id)
    {
        if (_context.Students == null)
        {
            return NotFound();
        }

        var signModel = await _context.Students.FindAsync(id);

        if (signModel == null)
        {
            return NotFound();
        }

        return signModel;
    }

    // PUT: api/SignApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSignModel(string id, SignModel signModel)
    {
        if (id != signModel.Id)
        {
            return BadRequest();
        }

        _context.Entry(signModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SignModelExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/SignApi
    // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<SignModel>> PostSignModel(SignModel signModel)
    {
        if (_context.Students == null)
        {
            return Problem("Entity set 'SignContext.Students'  is null.");
        }

        _context.Students.Add(signModel);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (SignModelExists(signModel.Id))
            {
                return Conflict();
            }

            throw;
        }

        return CreatedAtAction("GetSignModel", new { id = signModel.Id }, signModel);
    }

    // DELETE: api/SignApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSignModel(string id)
    {
        if (_context.Students == null)
        {
            return NotFound();
        }

        var signModel = await _context.Students.FindAsync(id);
        if (signModel == null)
        {
            return NotFound();
        }

        _context.Students.Remove(signModel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SignModelExists(string id)
    {
        return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}