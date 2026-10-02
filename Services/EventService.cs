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
    //Используем Dictionary, т.к Id не совпадает с [индексом]
    private readonly Dictionary<int,Event> _events = new();
    private int _newId = 1;
    public List<Event> GetEvents() => _events.Values.ToList();
    public Event? GetEvent(int id) => _events.GetValueOrDefault(id);
    public Event AddEvent(Event evt)
    {
        //Не доверяем evt.Id, пришедшему на вход, всегда генерим новый.
        //При добавлении DTO evt.Id придёт пустым, т.к в EventRequest его нет
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
        //Не доверяем evt.Id, пришедшему на вход, обновляем по id.
        //При добавлении DTO evt.Id придёт пустым, т.к в EventRequest его нет
        evt.Id = id;
        _events[id] = evt;
        return evt;
    }
    public bool RemoveEvent(int id) => _events.Remove(id);
}