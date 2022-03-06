using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace IgCloneMono.Api.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class Player
    {
        [Key]
        [Required]
        public long PlayerId { get; set; }
        
        [StringLength(20)]
        [Required]
        public string Username { get; set; }
        
        [StringLength(50)]
        [Required]
        public string Fullname { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string ImageUrl { get; set; }
        
        public string Bio { get; set; }
        
        [Required]
        public long DateOfBirth { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public bool Banned { get; set; }
        
        [Required]
        public bool Deleted { get; set; }
        
        [Required]
        public long CreatedAt { get; set; }
    }

    public class PlayerGetDto
    {
        public long PlayerId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string ImageUrl { get; set; }
        public string Bio { get; set; }
        public long DateOfBirth { get; set; }
        public string Email { get; set; }
        public long CreatedAt { get; set; }
    }

    public class PlayerCreateUpdateDto
    {
        [StringLength(20)]
        [Required]
        public string Username { get; set; }
        
        [StringLength(50)]
        [Required]
        public string Fullname { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
        
        public string Bio { get; set; }
        
        [Required]
        public long DateOfBirth { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class PlayerLoginDto
    {
        [StringLength(20)]
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}