using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data
{
    public sealed class BookShopContext : DbContext
    {
        #warning все эти проперти не обязательные перечислять тут. можно вместо обращения к пропертям (а у тебя к ним и так нет ообращений) 
        #warning использовать Set<Book> например
        public DbSet<ShopState> ShopStates { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookInstance> BookInstances { get; set; }
        public DbSet<DiscountByGenre> DiscountsByGenre { get; set; }

        public BookShopContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public async Task<T> GetOrCreate<T>(T compared) where T : class, IEquatable<T>
        {
            var entity = await Set<T>().FirstOrDefaultAsync(e => e.Equals(compared));
            if (entity is null)
            {
                entity = compared;
                Set<T>().Add(entity);
                await SaveChangesAsync();
            }

            return entity;
        }

        public async Task<T> GetById<T>(int id) where T : class
        {
            return await Set<T>().FindAsync(id);
        }

        public async Task RemoveById<T>(int id) where T : class
        {
            var entity = await GetById<T>(id);
            if (entity != null)
            {
                Set<T>().Remove(entity);
                await SaveChangesAsync();
            }
        }

        public async Task Remove<T>(T entity) where T : class
        {
            Set<T>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IList<BookInstance>> GetBooks(int stateId, int? offset = null, int? count = null)
        {
            var bookInstances = Set<BookInstance>()
                .Include(i => i.Book)
                .ThenInclude(b => b.Authors)
                .Include(i => i.Book)
                .ThenInclude(b => b.Genres)
                .Where(i => i.ShopStateId == stateId);

            if (offset != null)
            {
                bookInstances = bookInstances.Skip(offset.Value);
            }

            if (count != null)
            {
                bookInstances = bookInstances.Take(count.Value);
            }

            return await bookInstances.ToListAsync();
        }

        public async Task<int> CountBooks(int stateId)
        {
            return await Set<BookInstance>()
                .Where(i => i.ShopStateId == stateId)
                .CountAsync();
        }

        public async Task<T> Create<T>(T entity) where T : class
        {
            await Set<T>().AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public IList<TResult> GetOrCreateMany<TSource, TResult>(
            IEnumerable<TSource> collection,
            Func<TSource, TResult> transform)
            where TResult : class, IEquatable<TResult>
        {
            return collection.Select(async s => await GetOrCreate(transform(s)))
                .Select(t => t.Result)
                .ToList();
        }

        public async Task<ShopState> GetShopState(int shopStateId)
        {
            var shopStates = Set<ShopState>().Include(s => s.Discounts);
            return await shopStates.FirstOrDefaultAsync(s => s.Id == shopStateId);
        }

        public void EnsureShopStateCreated(int shopStateId)
        {
            var state = Set<ShopState>().Find(shopStateId);
            if (state is null)
            {
                state = new ShopState
                {
                    Capacity = 100
                };
                Set<ShopState>().Add(state);
                SaveChanges();
            }
        }
    }
}