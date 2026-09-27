/*
GET /events — получить список всех событий;
GET /events/{id} — получить событие по id; если не найдено — вернуть корректный HTTP-ответ (например, 404);
POST /events — создать событие, возвращать корректный HTTP-ответ (например, 201);
PUT /events/{id} — обновить событие целиком; если не найдено — вернуть корректный HTTP-ответ (например, 404);
DELETE /events/{id} — удалить событие; если не найдено — вернуть корректный HTTP-ответ (например, 404).
*/

using YaEventService.Models;

namespace YaEventService.Services;

public class EventService : IEventService
{
    //Список событий в памяти, как временное решение
    //Используем Dictionary, т.к Guid не совпадает с [индексом]
    private readonly Dictionary<int,Event> _events = new();
    private int _newId = 1;
    public List<Event> GetEvents() => _events.Values.ToList();
    public Event? GetEvent(int id) => _events.GetValueOrDefault(id);
    public Event AddEvent(Event evt)
    {
        //if (_events.ContainsKey(evt.Id)) {return null;} - закомментим
        //Не доверяем Id, пришедшему на вход, всегда генерим новый.
        evt.Id = _newId++;
        _events[evt.Id] = evt;
        return evt;
    }
    public Event? ChangeEvent(int id, Event evt)
    {
        if (!_events.ContainsKey(id))
        {
            return null;
        }
        evt.Id = id;
        _events[id] = evt;
        return evt;
    }
    public bool RemoveEvent(int id) => _events.Remove(id);
}