using AleksMediaEmailSystem.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AleksMediaEmailSystem.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet properties for each entity
        public DbSet<Client> Clients { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important for Identity

            // Configure Client entity
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(255);
                entity.Property(c => c.IsActive).IsRequired();
            });

            // Configure EmailTemplate entity
            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.TemplateName).IsRequired().HasMaxLength(200);
                entity.Property(t => t.Subject).IsRequired();
                entity.Property(t => t.Body).IsRequired();
            });

            // Configure Campaign entity
            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.CampaignName).IsRequired().HasMaxLength(200);
                entity.Property(c => c.Description).IsRequired();  // Ensure Description is populated
                entity.Property(c => c.StartDate).IsRequired();
                entity.Property(c => c.EndDate).IsRequired();
                entity.Property(c => c.XmlData).IsRequired(); // Ensure XmlData is populated

                // Define foreign key relationship
                entity.HasOne<EmailTemplate>()
                    .WithMany()
                    .HasForeignKey(c => c.EmailTemplateId)
                    .OnDelete(DeleteBehavior.Restrict); // Adjust as needed
            });

            //// Seed data (optional)
            //modelBuilder.Entity<Client>().HasData(
            //    new Client { Id = 1, Name = "Client 1", Email = "client1@example.com", IsActive = true, EmailConfiguration = string.Empty },
            //    new Client { Id = 2, Name = "Client 2", Email = "client2@example.com", IsActive = false, EmailConfiguration = string.Empty }
            //);

            //modelBuilder.Entity<EmailTemplate>().HasData(
            //    new EmailTemplate { Id = 1, TemplateName = "Welcome Email", Subject = "Welcome!", Body = "Hello, welcome to our service!", 
            //    CreatedAt = DateTime.Now, IsActive = true,
            //        UpdatedAt = DateTime.Now
            //    }
            //);

            //// Ensure seed data includes all required fields
            //modelBuilder.Entity<Campaign>().HasData(
            //    new Campaign
            //    {
            //        Id = 1,
            //        CampaignName = "Summer Sale",
            //        Description = "Special discounts on summer products.",  // Ensure Description is populated
            //        EndDate = DateTime.Now.AddDays(1),
            //        XmlData = "<Clients>\r\n\r\n    <Client ID=\"12345\">\r\n\r\n        <Template Id=\"1\">\r\n\r\n            <Name>TemplateName.html</Name>\r\n\r\n            <MarketingData>{json data string representation}</MarketingData>\r\n\r\n        </Template>\r\n\r\n    </Client>\r\n\r\n    <Client ID=\"54321\">\r\n\r\n        <Template Id=\"2\">\r\n\r\n            <Name>TemplateName2.html</Name>\r\n\r\n            <MarketingData>{json data string representation}</MarketingData>\r\n\r\n        </Template>\r\n\r\n    </Client>\r\n\r\n    <!-- More clients -->\r\n\r\n</Clients>",
            //        EmailTemplateId = 1
            //    },
            //    new Campaign
            //    {
            //        Id = 2,
            //        CampaignName = "Winter Wonderland",
            //        Description = "Offers on winter essentials.",  // Ensure Description is populated,
            //        EndDate = DateTime.Now.AddDays(1),
            //        XmlData = "<Clients>\r\n\r\n    <Client ID=\"12345\">\r\n\r\n        <Template Id=\"1\">\r\n\r\n            <Name>TemplateName.html</Name>\r\n\r\n            <MarketingData>{json data string representation}</MarketingData>\r\n\r\n        </Template>\r\n\r\n    </Client>\r\n\r\n    <Client ID=\"54321\">\r\n\r\n        <Template Id=\"2\">\r\n\r\n            <Name>TemplateName2.html</Name>\r\n\r\n            <MarketingData>{json data string representation}</MarketingData>\r\n\r\n        </Template>\r\n\r\n    </Client>\r\n\r\n    <!-- More clients -->\r\n\r\n</Clients>",
            //        EmailTemplateId = 1
            //    }
            //);
        }
    }
}
