using auth.Models;
using Microsoft.EntityFrameworkCore;
namespace auth.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options){

        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder){
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<User> users {get;set;}
        public DbSet<Role> roles {get;set;}

    }
}