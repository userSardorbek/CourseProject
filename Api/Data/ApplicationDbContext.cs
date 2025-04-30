using Api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Answer> Answers { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Form> Forms { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TemplateAccess> TemplateAccesses { get; set; }
    public DbSet<Template> Templates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        List<IdentityRole> roles = new List<IdentityRole>()
        {
            new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole() { Name = "User", NormalizedName = "USER" },
        };
        modelBuilder.Entity<IdentityRole>().HasData(roles);
        
        modelBuilder.Entity<Like>()
            .HasIndex(l => new { l.UserId, l.TemplateId })
            .IsUnique();
        
        // Configure many-to-many relationship between Template and Tag
        modelBuilder.Entity<Template>()
            .HasMany(t => t.Tags)
            .WithMany(tg => tg.Templates)
            .UsingEntity(j => j.ToTable("TemplateTags"));

        // Configure foreign key relationships
        modelBuilder.Entity<Template>()
            .HasOne(t => t.Creator)
            .WithMany(u => u.CreatedTemplates)
            .HasForeignKey(t => t.CreatorId)
            .OnDelete(DeleteBehavior.Cascade); // Example of onDelete behavior

        modelBuilder.Entity<Question>()
            .HasOne(q => q.Template)
            .WithMany(t => t.Questions)
            .HasForeignKey(q => q.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Form>()
            .HasOne(f => f.User)
            .WithMany(u => u.FilledForms)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Form>()
            .HasOne(f => f.Template)
            .WithMany(t => t.Forms)
            .HasForeignKey(f => f.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Form)
            .WithMany(f => f.Answers)
            .HasForeignKey(a => a.FormId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Question)
            .WithMany(q => q.Answers)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Author)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Template)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Like>()
            .HasOne(l => l.User)
            .WithMany(u => u.Likes)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Like>()
            .HasOne(l => l.Template)
            .WithMany(t => t.Likes)
            .HasForeignKey(l => l.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TemplateAccess>()
            .HasOne(ta => ta.Template)
            .WithMany(t => t.AllowedUsers)
            .HasForeignKey(ta => ta.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TemplateAccess>()
            .HasOne(ta => ta.User)
            .WithMany(u => u.AccessibleTemplates)
            .HasForeignKey(ta => ta.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}