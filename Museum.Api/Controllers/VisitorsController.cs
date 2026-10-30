using Microsoft.AspNetCore.Mvc;
using Museum.Api.Storage;
using Museum.Domain;

namespace Museum.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VisitorsController(MuseumStore store) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Visitor>> GetAll()
    {
        return store.Visitors;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Visitor> GetById(int id)
    {
        var visitor = store.Visitors.FirstOrDefault(v => v.Id == id);
        if (visitor is null)
        {
            return NotFound();
        }

        return visitor;
    }

    [HttpPost]
    public ActionResult<Visitor> Create(Visitor visitor)
    {
        visitor.Id = store.Visitors.Select(v => v.Id).DefaultIfEmpty(0).Max() + 1;
        store.Visitors.Add(visitor);

        return CreatedAtAction(nameof(GetById), new { id = visitor.Id }, visitor);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Visitor> Update(int id, Visitor updated)
    {
        var visitor = store.Visitors.FirstOrDefault(v => v.Id == id);
        if (visitor is null)
        {
            return NotFound();
        }

        visitor.FullName = updated.FullName;
        visitor.Phone = updated.Phone;
        visitor.BirthDate = updated.BirthDate;

        return visitor;
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var visitor = store.Visitors.FirstOrDefault(v => v.Id == id);
        if (visitor is null)
        {
            return NotFound();
        }

        if (store.Tickets.Any(t => t.Visitor.Id == id))
        {
            return Conflict("У посетителя есть билеты");
        }

        store.Visitors.Remove(visitor);

        return NoContent();
    }
}
