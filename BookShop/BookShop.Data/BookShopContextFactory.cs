namespace BookShop.Data
{
    public sealed class BookShopContextFactory
    {
        private readonly string _connectionString;

        public BookShopContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BookShopContext GetContext()
        {
            return new BookShopContext(BookShopContextDesignTimeFactory.GetSqlServerOptions(_connectionString));
        }
    }
}
