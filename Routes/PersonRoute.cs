//Armazenar todas as rotas
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;

namespace Person.Routes;

public static class PersonRoute // Em virtude da Classe ser estática os membros da classe devem ser estáticos também.
{

    public static void PersonRoutes(this WebApplication app) // o this caracteriza como um extension method
    {

        var route = app.MapGroup("person"); // todos os métodos possuem a mesma rota, portanto irei criar um MapGroup para agrupar.
        route.MapPost("",
            async (PersonRequest req, PersonContext context) =>
        {
            var person = new PersonModel(req.name);
            await context.AddAsync(person);
            await context.SaveChangesAsync();
        }
        );
        route.MapGet("",
        async (PersonContext context) =>
        {
            var people = await context.People.ToListAsync();
            return Results.Ok(people);
        });
        route.MapPut("{id:guid}", async (Guid id, PersonRequest req, PersonContext context) =>
        {

            var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
                return Results.NotFound();

            person.ChangeName(req.name);
            await context.SaveChangesAsync();
            return Results.Ok(person);

        });
        route.MapDelete("{id:guid}", async (Guid id, PersonContext context) =>
        {
            var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
                return Results.NotFound();

            person.SetInactive();

            await context.SaveChangesAsync();
            return Results.Ok(person);

        });

    }

}