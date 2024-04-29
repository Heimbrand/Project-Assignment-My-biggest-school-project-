using HagaDropsIt.Client.Services;
using HagaDropsIt.Shared.DTOs;
using HagaDropsIt.Shared.Interfaces;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace HagaDropsIt.Client.ChatBot.Plugins
{
    public class EventPlugin 
    {
        private IEventService<EventDto> _eventService;

        public EventPlugin(IEventService<EventDto> eventService)
        {
            _eventService = eventService;
        }

        [KernelFunction, Description("Get all events")]
        public Task<IEnumerable<EventDto>> GetAllEvents()
        {
            return _eventService.GetAllEvents();
        }

        [KernelFunction, Description("Get event by ID")]
        public Task<EventDto> GetEventById(int id)
        {
            return _eventService.GetEventById(id);
        }

        [KernelFunction, Description("Get events by name")]
        public Task<IEnumerable<EventDto>> GetEventsByName(string name)
        {
            return _eventService.GetEventsByName(name);
        }

        [KernelFunction, Description("Add a new event")]
        public Task AddEvent(EventDto newEvent)
        {
            return _eventService.AddEvent(newEvent);
        }

        [KernelFunction, Description("Delete an event by ID")]
        public Task DeleteEvent(int id)
        {
            return _eventService.DeleteEvent(id);
        }

        [KernelFunction, Description("Update an event")]
        public Task UpdateEvent(EventDto updateEvent)
        {
            return _eventService.UpdateEvent(updateEvent);
        }
    }
}
