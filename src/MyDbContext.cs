using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace FileToData
{
    /// <summary>
    /// Represents a session with the database for managing uses,orders, and products.
    /// </summary>
    /// <remarks>
    /// This class is a custom <see cref="DbContext"/> that includes  <see cref="DbSet{User}"/>, <see cref="DbSet{Order}"/>, <see cref="DbSet{Product}"/>, and <see cref="DbSet{ProductOrder}"/>
    /// for managing <see cref="User"/>, <see cref="Order"/>, <see cref="Product"/>, and <see cref="ProductOrder"/> entities.
    /// It is configured to use the specified database provider and connection string, which are passed through the <see cref="DbContextOptions{MyDbContext}"/> parameter.
    /// </remarks>
    public class MyDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyDbContext"/> class.
        /// </summary>
        public MyDbContext() { }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{User}"/> that can be used to query and save instances of <see cref="User"/>.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Order}"/> that can be used to query and save instances of <see cref="Order"/>.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Product}"/> that can be used to query and save instances of <see cref="Product"/>.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{ProductOrder}"/> that can be used to query and save instances of <see cref="ProductOrder"/>.
        /// </summary>
        public DbSet<ProductOrder> ProductOrders { get; set; }

        /// <summary>
        /// Configures the database context options, including the connection string 
        /// and naming conventions, using PostgreSQL as the database provider.
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for the DbContext.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();

            string connectionString = configuration.GetConnectionString("PostgreSQL");
            optionsBuilder.UseNpgsql(connectionString).UseLowerCaseNamingConvention().UseSnakeCaseNamingConvention();
        }

    }
}
