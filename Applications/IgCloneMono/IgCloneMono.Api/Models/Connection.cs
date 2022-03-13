using System.ComponentModel.DataAnnotations;

namespace IgCloneMono.Api.Models
{
    public class Connection
    {
        [Key]
        [Required]
        public long ConnectionId { get; set; }
        
        [Required]
        public long PlayerId { get; set; }
        
        [Required]
        public long FollowerId { get; set; }
        
        [Required]
        public long CreatedAt { get; set; }
        
        [Required]
        public bool Deleted { get; set; }
    }
}