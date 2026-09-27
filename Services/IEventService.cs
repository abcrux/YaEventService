/*
GET /events — получить список всех событий;
GET /events/{id} — получить событие по id; если не найдено — вернуть корректный HTTP-ответ (например, 404);
POST /events — создать событие, возвращать корректный HTTP-ответ (например, 201);
PUT /events/{id} — обновить событие целиком; если не найдено — вернуть корректный HTTP-ответ (например, 404);
DELETE /events/{id} — удалить событие; если не найдено — вернуть корректный HTTP-ответ (например, 404).
*/

using YaEventService.Models;

namespace YaEventService.Services;

public interface IEventService
{
    List<Event> GetEvents();
    Event? GetEvent(int id);
    Event AddEvent(Event evt);
    Event? ChangeEvent(int id, Event evt);
    bool RemoveEvent(int id);
}