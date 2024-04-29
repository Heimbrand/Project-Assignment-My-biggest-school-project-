using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;
using HagaDropsIt.SQL.Store.Repositories;

namespace HagaDropsIt.API.Extensions;

public static class EventEndpointExtensions
{
    public static IEndpointRouteBuilder MapEventEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/events");

        group.MapGet("/", GetAllEvents);
        group.MapGet("/{id}", GetEventById);
        group.MapGet("/search/{name}", GetEventByName);
        group.MapPost("/", AddEvent);
        group.MapDelete("/{id}", DeleteEvent);
        group.MapPut("/", UpdateEvent);

        return app;
    }

    private static async Task<IResult> GetAllEvents(IEventService<Event> eRepo)
    {
        var getEvents = await eRepo.GetAllEvents();

        return Results.Ok(getEvents);
    }

    private static async Task<IResult> GetEventById(IEventService<Event> eRepo, int id)
    {
        var getEvent = await eRepo.GetEventById(id);
        if (getEvent == null)
        {
            return Results.NotFound($"No event exists with Id: {id}");
        }

        return Results.Ok(eRepo);
    }

    private static async Task<IResult> GetEventByName(IEventService<Event> eRepo, string name)
    {
        var getEvent = await eRepo.GetEventsByName(name);

        if (getEvent == null)
        {
            return Results.NotFound($"No event exists with the name: {name}");
        }

        return Results.Ok(getEvent);
    }

    private static async Task<IResult> AddEvent(IEventService<Event> eRepo, Event newEvent)
    {
        if (newEvent == null)
        {
            return Results.BadRequest();
        }

        var createEvent = new Event
        {
            Name = newEvent.Name,
            AgeRestriction = newEvent.AgeRestriction,
            StartDate = newEvent.StartDate,
            EndDate = newEvent.EndDate,
            Description = newEvent.Description,
            ImageURL = newEvent.ImageURL,
            Location = newEvent.Location,
            Price = newEvent.Price
        };

        await eRepo.AddEvent(createEvent);
        return Results.Ok(createEvent);
    }

    private static async Task<IResult> DeleteEvent(IEventService<Event> eRepo, int id)
    {
        var deleteEvent = await eRepo.GetEventById(id);

        if (deleteEvent == null)
        {
            return Results.NotFound($"No event with the Id: {id}");
        }

        await eRepo.DeleteEvent(id);
        return Results.Ok();
    }

    private static async Task<IResult> UpdateEvent(IEventService<Event> eRepo, Event events)
    {
        var getEvent = await eRepo.GetEventById(events.Id);

        if (events == null)
        {
            return Results.BadRequest();
        }

        await eRepo.UpdateEvent(events);
        return Results.Ok();
    }
}
