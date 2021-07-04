using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookShop.Data
{
    public sealed class BookShopContextDesignTimeFactory : IDesignTimeDbContextFactory<BookShopContext>
    {
        private const string DefaultConnectionString =
            "Server=localhost\\SQLEXPRESS;Database=BookShop;Password=zeratul;Trusted_Connection=True;";

        public static DbContextOptions<BookShopContext> GetSqlServerOptions(string? connectionString = null)
        {
            return new DbContextOptionsBuilder<BookShopContext>()
                .UseSqlServer(connectionString ?? DefaultConnectionString)
                .Options;
        }

        public BookShopContext CreateDbContext(string[] args)
        {
            return new BookShopContext(GetSqlServerOptions());
        }
    }
}