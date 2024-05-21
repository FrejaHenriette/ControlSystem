using System.Collections.Immutable;
using System.Reflection;
using ControlSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlSystem.Infrastructure.Persistence;

public class ControlSystemDbContext(DbContextOptions<ControlSystemDbContext> options) : DbContext(options)
{
    public virtual DbSet<TransportOrder> TransportOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Employee>()
            .HasMany(employee => employee.TransportOrders)
            .WithOne(t => t.Employee)
            .HasForeignKey(t => t.EmployeeId);
        
        modelBuilder.Entity<TransportOrder>()
            .HasOne(t => t.Origin)
            .WithMany(l => l.TransportOrderOrigin)
            .HasForeignKey(t => t.OriginId);

        modelBuilder.Entity<TransportOrder>()
            .HasOne(t => t.Destination)
            .WithMany(l => l.TransportOrderDestination)
            .HasForeignKey(t => t.DestinationId);

        modelBuilder.Entity<TransportOrderItem>()
            .HasKey(toi => new { toi.TransportOrderId, toi.ItemId });
        
        modelBuilder.Entity<TransportOrderItem>()
            .HasOne(toi => toi.TransportOrder)
            .WithMany(t => t.TransportOrderItem)
            .HasForeignKey(toi => toi.TransportOrderId);
        
        modelBuilder.Entity<TransportOrderItem>()
            .HasOne(toi => toi.Item)
            .WithMany(i => i.TransportOrderItem)
            .HasForeignKey(toi => toi.ItemId);
    }
}