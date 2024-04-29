using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HagaDropsIt.SQL.Store.Repositories;

public class EventRepository(StoreDbContext context) : IEventService<Event>
{
    public async Task<IEnumerable<Event>> GetAllEvents()
    {
        return await context.Events.ToListAsync();
    }

    public async Task<Event?> GetEventById(int id)
    {
        return await context.Events.FindAsync(id);
    }

    public async Task<IEnumerable<Event>> GetEventsByName(string name)
    {
        return await context.Events.Where(n => n.Name.Contains(name)).ToListAsync();
    }

    public async Task AddEvent(Event newEvent)
    {
        await context.Events.AddAsync(newEvent);
        await context.SaveChangesAsync();
    }

    public async Task DeleteEvent(int id)
    {
        var dEvent = await context.Events.FindAsync(id);
        if (dEvent == null)
        {
            return;
        }

        context.Events.Remove(dEvent);
        await context.SaveChangesAsync();
    }

    public async Task<bool> UpdateEvent(Event updatedEvent)
    {
        var eventToUpdate = await context.Events.FindAsync(updatedEvent.Id);
        if (eventToUpdate == null)
        {
            await context.AddAsync(updatedEvent);
            await context.SaveChangesAsync();
            return true;
        }

        eventToUpdate.Name = updatedEvent.Name;
        eventToUpdate.Description = updatedEvent.Description;
        eventToUpdate.StartDate = updatedEvent.StartDate;
        eventToUpdate.EndDate = updatedEvent.EndDate;
        eventToUpdate.ImageURL = updatedEvent.ImageURL;
        eventToUpdate.Location = updatedEvent.Location;
        eventToUpdate.Price = updatedEvent.Price;
        eventToUpdate.AgeRestriction = updatedEvent.AgeRestriction;

        await context.SaveChangesAsync();
        return true;
    }


}