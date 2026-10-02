using YaEventService.Models;

namespace YaEventService.Contracts;

public static class EventMappings
{
    public static Event ToDomain(this EventRequest request)
    {
        return new Event 
        { 
            Id = 0,
            Title = request.Title,
            Description = request.Description,
            StartAt = request.StartAt,
            EndAt = request.EndAt
         };
    }

    public static EventResponse ToResponse(this Event evt)
    {
        return new EventResponse
        {
            Id = evt.Id,
            Title = evt.Title,
            Description = evt.Description,
            StartAt = evt.StartAt,
            EndAt = evt.EndAt            
        };
    }
}