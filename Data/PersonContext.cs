using Microsoft.EntityFrameworkCore;
using Person.Models;

namespace Person.Data;

public class PersonContext : DbContext
{
    public DbSet<PersonModel> People { get; set; } // representação da tabela em memória

    protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
    {
        optionsbuilder.UseSqlite("Data Source=person.sqlite");
        base.OnConfiguring(optionsbuilder);
    }

}