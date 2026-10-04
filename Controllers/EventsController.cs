/*
GET /events — получить список всех событий;
GET /events/{id} — получить событие по id; если не найдено — вернуть корректный HTTP-ответ (например, 404);
POST /events — создать событие, возвращать корректный HTTP-ответ (например, 201);
PUT /events/{id} — обновить событие целиком; если не найдено — вернуть корректный HTTP-ответ (например, 404);
DELETE /events/{id} — удалить событие; если не найдено — вернуть корректный HTTP-ответ (например, 404).
*/

using Microsoft.AspNetCore.Mvc;
using YaEventService.Contracts;
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
    public ActionResult<List<EventResponse>> GetEvents()
    {
        var evts = _eventService.GetEvents();
        var evtsResp = evts.Select(evt => evt.ToResponse()).ToList();
        return Ok(evtsResp); //200
    }

    // GET /events/{id}
    [HttpGet("{id:int}")]
    public ActionResult<EventResponse> GetEvent(int id)
    {
        var evt = _eventService.GetEvent(id);
        if (evt is null) 
        {
            return NotFound(); //404
        }
        return Ok(evt.ToResponse()); //200
    }

    // POST /events
    [HttpPost]
    public ActionResult<EventResponse> CreateEvent([FromBody] EventRequest evtReq)
    {
        var evt = evtReq.ToDomain();
        var evtCreated = _eventService.AddEvent(evt);
        return CreatedAtAction(nameof(GetEvent), new {id = evtCreated.Id}, evtCreated.ToResponse()); //201
    }

    // PUT /events/{id}
    [HttpPut("{id:int}")]
    public ActionResult<EventResponse> UpdateEvent(int id, [FromBody] EventRequest evtReq)
    {
        var evt = evtReq.ToDomain();
        var evtUpd = _eventService.ChangeEvent(id, evt);
        if (evtUpd is null)
        {
            return NotFound(); //404
        }
        return Ok(evtUpd.ToResponse()); //200
    }

    // DELETE /events/{id}
    [HttpDelete("{id:int}")]
    public IActionResult DeleteEvent(int id)
    {
        var evtRemoved = _eventService.RemoveEvent(id);
        if (!evtRemoved)
            return NotFound(); //404

        return NoContent(); //204
    }
}