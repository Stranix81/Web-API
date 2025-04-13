using Microsoft.EntityFrameworkCore;
using Core.Models;
using Core.Models.FunctionsReturnModels;
using Core.Enums;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> clients { get; set; }

        public DbSet<Order> orders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        [DbFunction("get_total_cost_of_birthday_orders", Schema = "schema_1")]
        public IQueryable<BdaySums> GetCostsBdays()
        {
            return FromExpression(() => GetCostsBdays());
        }

        [DbFunction("get_avg_costs_by_hour", Schema = "schema_1")]
        public IQueryable<AvgCostsByHour> GetAvgCostsByHour()
        {
            return FromExpression(() => GetAvgCostsByHour());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("schema_1");
            modelBuilder.HasPostgresEnum<OrderStatus>("schema_1", "order_status");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client");
                entity.HasKey(c => c.id);

                entity.Property(c => c.name)
                .HasColumnType("varchar");

                entity.Property(c => c.lastname)
                .HasColumnType("varchar");

                entity.Property(c => c.birth_date)
                .HasColumnType("date");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");
                entity.HasKey(o => o.id);

                entity.Property(o => o.cost)
                .HasColumnType("numeric");

                entity.Property(o => o.date)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnType("date");

                entity.Property(o => o.time)
                .HasDefaultValueSql("CURRENT_TIME")
                .HasColumnType("time");

                entity.Property(o => o.client_id)
                .HasColumnType("integer");

                entity.Property(o => o.status)
                .HasConversion<string>()
                .HasColumnType("order_status");

                entity.HasOne(o => o.client)
                      .WithMany()
                      .HasForeignKey(o => o.client_id)
                      .IsRequired()
                      .HasConstraintName("order_client_id_fk")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<BdaySums>(entity =>
            {
                entity.ToFunction("get_total_cost_of_birthday_orders");
                entity.HasNoKey();
            });

            modelBuilder.Entity<AvgCostsByHour>(entity =>
            {
                entity.ToFunction("get_avg_costs_by_hour");
                entity.HasNoKey();
            });
        }
    }
}
