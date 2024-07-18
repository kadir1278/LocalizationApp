using LocalizationApp.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LocalizationApp.Models
{
    public class SystemContext : DbContext
    {
        private readonly IMemoryCache _memoryCache;

        public SystemContext(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public DbSet<LocalizationModel> LocalizationModels { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationHelper.GetSqlConnectionString());
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is LocalizationModel localization && entry.State is (EntityState.Added or EntityState.Modified or EntityState.Deleted))
                {
                    if (localization.CultureInfo is not null)
                    {
                        string cacheKey = $"LocalizationCookie{localization.CultureInfo}";
                        _memoryCache.Remove(cacheKey);
                        if (!_memoryCache.TryGetValue(cacheKey, out _))
                        {
                            _memoryCache.Set(cacheKey, LocalizationModels.ToList().Append(localization), DateTimeOffset.MaxValue);
                        }
                    }

                }
            }
            return base.SaveChanges();
        }
    }
}
