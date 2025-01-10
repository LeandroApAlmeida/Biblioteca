using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data {


    public class ApplicationDbContext : DbContext {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRoleModel>().HasData(
                
                new UserRoleModel { Id = 1, Description = "Administrador" },
                
                new UserRoleModel { Id = 2, Description = "Convidado" }

            );

        }


        public DbSet<BookModel> Books { get; set; }


        public DbSet<DiscardedBookModel> DiscardedBooks { get; set; }


        public DbSet<DonatedBookModel> DonatedBooks { get; set; }


        public DbSet<LoanModel> Loans { get; set; }


        public DbSet<PersonModel> Persons { get; set; }


        public DbSet<SessionModel> Sessions { get; set; }


        public DbSet<UserModel> Users { get; set; }


        public DbSet<UserRoleModel> UserRoles { get; set; }


    }


}