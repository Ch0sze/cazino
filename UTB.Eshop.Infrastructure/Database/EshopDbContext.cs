using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTB.Eshop.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using UTB.Eshop.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace UTB.Eshop.Infrastructure.Database
{
    public class EshopDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Carousel> Carousels { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public EshopDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DatabaseInit dbInit = new DatabaseInit();
            modelBuilder.Entity<Carousel>().HasData(dbInit.GetCarousel());
            modelBuilder.Entity<Product>().HasData(dbInit.GetProducts());


            //Identity - User and Role initialization
            //roles must be added first
            modelBuilder.Entity<Role>().HasData(dbInit.CreateRoles());

            //then, create users ..
            (User admin, List<IdentityUserRole<int>> adminUserRoles) = dbInit.CreateAdminWithRoles();
            (User manager, List<IdentityUserRole<int>> managerUserRoles) = dbInit.CreateManagerWithRoles();

            //.. and add them to the table
            modelBuilder.Entity<User>().HasData(admin, manager);

            //and finally, connect the users with the roles
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(adminUserRoles);
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(managerUserRoles);
        }
    }
}
