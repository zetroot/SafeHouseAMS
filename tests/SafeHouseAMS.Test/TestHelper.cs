using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SafeHouseAMS.DataLayer;

namespace SafeHouseAMS.Test;

internal static class TestHelper
{
    public static DataContext CreateInMemoryDatabase()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var dbctxOptsBuilder = new DbContextOptionsBuilder()
            .UseLazyLoadingProxies()
            .UseSqlite(connection, opt =>
                opt.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
        var ctx = new DataContext(dbctxOptsBuilder.Options);
        ctx.Database.EnsureDeleted();
        ctx.Database.EnsureCreated();
        return ctx;
    }

    public static IMapper CreateMapper()
    {
        var cfg = new MapperConfiguration(c => c.AddMaps(typeof(DataContext).Assembly));
        return new Mapper(cfg);
    }
}
