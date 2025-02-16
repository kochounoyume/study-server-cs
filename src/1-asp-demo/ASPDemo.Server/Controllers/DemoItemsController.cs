using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPDemo.Server.Models;
using ASPDemo.Shared.Models;

namespace ASPDemo.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DemoItemsController(DemoContext context) : ControllerBase
{
    // GET: api/DemoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DemoItem>>> GetTodoItems()
    {
        return await context.demoItems.ToListAsync();
    }

    // GET: api/DemoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DemoItem>> GetDemoItem(long id)
    {
        var demoItem = await context.demoItems.FindAsync(id);

        if (demoItem == null)
        {
            return NotFound();
        }

        return demoItem;
    }

    // PUT: api/DemoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDemoItem(long id, DemoItem demoItem)
    {
        if (id != demoItem.id)
        {
            return BadRequest();
        }

        context.Entry(demoItem).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DemoItemExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/DemoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<DemoItem>> PostDemoItem(DemoItem demoItem)
    {
        context.demoItems.Add(demoItem);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDemoItem), new { id = demoItem.id }, demoItem);
    }

    // DELETE: api/DemoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDemoItem(long id)
    {
        var demoItem = await context.demoItems.FindAsync(id);
        if (demoItem == null)
        {
            return NotFound();
        }

        context.demoItems.Remove(demoItem);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool DemoItemExists(long id)
    {
        return context.demoItems.Any(e => e.id == id);
    }
}