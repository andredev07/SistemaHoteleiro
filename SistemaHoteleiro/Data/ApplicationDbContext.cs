using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaHoteleiro.Models;

namespace SistemaHoteleiro.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryRoom> CategoryRooms { get; set; }

        public DbSet<Guest> Guests { get; set; }

        public DbSet<Reserve> Reserves { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Product> Products {get; set; }

        public DbSet<Sale> Sales { get;set; }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>(room =>
            {
                room.HasOne(x => x.CategoryRoom)
                    .WithMany(categoryRoom => categoryRoom.Rooms)
                    .HasForeignKey(x => x.CategoryRoomId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Reserve>(reserve =>
           {
               reserve.HasOne(x => x.Guest)
                      .WithMany(guest => guest.Reserves)
                      .HasForeignKey(x => x.GuestId)
                      .OnDelete(DeleteBehavior.Cascade);
           });

            modelBuilder.Entity<Reserve>(reserve =>
            {
                reserve.HasOne(x => x.Room)
                       .WithMany(room => room.Reserves)
                       .HasForeignKey(x => x.RoomId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Sale>(sale => 
            {
                sale.HasOne(x => x.Product)
                .WithMany(product => product.Sales)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Sale>(sale => 
            {
                sale.HasOne(x => x.Reserve)
                .WithMany(reserve => reserve.Sales)
                .HasForeignKey(x => x.ReserveId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
