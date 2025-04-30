using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Model;

public class Template
{
        public long TemplateId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [MaxLength(100)]
        public string Topic { get; set; }
        
        public string ImageUrl { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public bool IsPublic { get; set; } = false;

        [ForeignKey("User")]
        public string CreatorId { get; set; }
        public User Creator { get; set; }


        public ICollection<Question> Questions { get; set; }
        public ICollection<Form> Forms { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<TemplateAccess> AllowedUsers { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();    
}