/*
GET /events — получить список всех событий;
GET /events/{id} — получить событие по id; если не найдено — вернуть корректный HTTP-ответ (например, 404);
POST /events — создать событие, возвращать корректный HTTP-ответ (например, 201);
PUT /events/{id} — обновить событие целиком; если не найдено — вернуть корректный HTTP-ответ (например, 404);
DELETE /events/{id} — удалить событие; если не найдено — вернуть корректный HTTP-ответ (например, 404).
*/

using Microsoft.AspNetCore.Mvc;
using YaEventService.Models;
using YaEventService.Services;

namespace YaEventService.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    // GET /events
    [HttpGet]
    public ActionResult<List<Event>> GetEvents()
    {
        return Ok(_eventService.GetEvents()); //200
    }

    // GET /events/{id}
    [HttpGet("{id:int}")]
    public ActionResult<Event> GetEvent(int id)
    {
        var evt = _eventService.GetEvent(id);
        if (evt is null) 
        {
            return NotFound(); //404
        }
        return Ok(evt); //200
    }

    // POST /events
    [HttpPost]
    public ActionResult<Event> CreateEvent([FromBody] Event evt)
    {
        var created = _eventService.AddEvent(evt);
        return CreatedAtAction(nameof(GetEvent), new {id = created.Id}, created); //201
    }

    // PUT /events/{id}
    [HttpPut("{id:guid}")]
    public ActionResult<Event> UpdateEvent(int id, [FromBody] Event evt)
    {
        var updated = _eventService.ChangeEvent(id, evt);
        if (updated is null)
        {
            return NotFound(); //404
        }
        return Ok(updated); //200
    }

    // DELETE /events/{id}
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteEvent(int id)
    {
        var removed = _eventService.RemoveEvent(id);
        if (!removed)
            return NotFound(); //404

        return NoContent(); //204
    }
}