using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Model;

[Table("users")]
[Index(nameof(Email), IsUnique = true)]
[Index(nameof(UserName), IsUnique = true)]
public class User : IdentityUser
{
    public override string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public override string UserName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public override string? PasswordHash { get; set; }

    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    
    public bool IsBlocked { get; set; } = false;

    public ICollection<Template> CreatedTemplates { get; set; }
    public ICollection<Form> FilledForms { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Like> Likes { get; set; }
    public ICollection<TemplateAccess> AccessibleTemplates { get; set; }
}