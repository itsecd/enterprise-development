using Microsoft.AspNetCore.Mvc;
using Museum.Api.Storage;
using Museum.Domain;

namespace Museum.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExhibitionsController(MuseumStore store) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Exhibition>> GetAll()
    {
        return store.Exhibitions;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Exhibition> GetById(int id)
    {
        var exhibition = store.Exhibitions.FirstOrDefault(e => e.Id == id);
        if (exhibition is null)
        {
            return NotFound();
        }

        return exhibition;
    }

    [HttpPost]
    public ActionResult<Exhibition> Create(Exhibition exhibition)
    {
        exhibition.Id = store.Exhibitions.Count == 0 ? 1 : store.Exhibitions.Max(e => e.Id) + 1;
        store.Exhibitions.Add(exhibition);

        return CreatedAtAction(nameof(GetById), new { id = exhibition.Id }, exhibition);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Exhibition> Update(int id, Exhibition updated)
    {
        var exhibition = store.Exhibitions.FirstOrDefault(e => e.Id == id);
        if (exhibition is null)
        {
            return NotFound();
        }

        exhibition.Name = updated.Name;
        exhibition.Theme = updated.Theme;
        exhibition.HallNumber = updated.HallNumber;
        exhibition.StartDate = updated.StartDate;
        exhibition.EndDate = updated.EndDate;

        return exhibition;
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var exhibition = store.Exhibitions.FirstOrDefault(e => e.Id == id);
        if (exhibition is null)
        {
            return NotFound();
        }

        if (store.Tours.Any(t => t.Exhibitions.Any(e => e.Id == id)))
        {
            return Conflict("Выставка используется в экскурсиях");
        }

        store.Exhibitions.Remove(exhibition);

        return NoContent();
    }
}
