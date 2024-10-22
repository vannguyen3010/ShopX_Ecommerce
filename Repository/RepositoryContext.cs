﻿using Entities.Identity;
using Entities.Models;
using Entities.Models.Address;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleAssignmentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());


            modelBuilder.Entity<AdministrativeRegion>().HasKey(ar => ar.Id);
            modelBuilder.Entity<AdministrativeUnit>().HasKey(au => au.Id);
            modelBuilder.Entity<Province>().HasKey(p => p.Code);
            modelBuilder.Entity<District>().HasKey(d => d.Code);
            modelBuilder.Entity<Ward>().HasKey(w => w.Code);


            // Configure relationships
            modelBuilder.Entity<Province>()
                .HasOne(p => p.AdministrativeRegion)
                .WithMany()
                .HasForeignKey(p => p.AdministrativeRegionId);

            modelBuilder.Entity<Province>()
                .HasOne(p => p.AdministrativeUnit)
                .WithMany()
                .HasForeignKey(p => p.AdministrativeUnitId);

            modelBuilder.Entity<District>()
                .HasOne(d => d.Province)
                .WithMany()
                .HasForeignKey(d => d.ProvinceCode);

            modelBuilder.Entity<District>()
                .HasOne(d => d.AdministrativeUnit)
                .WithMany()
                .HasForeignKey(d => d.AdministrativeUnitId);

            modelBuilder.Entity<Ward>()
                .HasOne(w => w.District)
                .WithMany()
                .HasForeignKey(w => w.DistrictCode);

            modelBuilder.Entity<Ward>()
                .HasOne(w => w.AdministrativeUnit)
                .WithMany()
                .HasForeignKey(w => w.AdministrativeUnitId);

        }

        public DbSet<Banner> Banners { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CateProduct> CateProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CommentProduct> CommentProducts { get; set; }
        public DbSet<BannerProduct> BannerProducts { get; set; }
        public DbSet<CategoryIntroduce> CategoryIntroduces { get; set; }
        public DbSet<Introduce> Introduces { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ProfileUser> ProfileUsers { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<ShippingCost> ShippingCosts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<SocialMediaInfo> SocialMediaInfos { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
