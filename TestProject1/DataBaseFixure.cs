using System;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace TestProject1
{
    public class DataBaseFixture : IDisposable
    {
        public ProductDbContext Context { get; private set; }

        public DataBaseFixture()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseSqlServer("Server=srv2\\pupils;Database=Tests_325530996;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;

            Context = new ProductDbContext(options);
            Context.Database.EnsureCreated();
        }
    

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}