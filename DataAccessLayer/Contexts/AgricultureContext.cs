using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Contexts
{
    public class AgricultureContext : IdentityDbContext
    {
        public AgricultureContext(DbContextOptions<AgricultureContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
