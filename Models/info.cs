using System.ComponentModel.DataAnnotations;

namespace RecipeSharingWebsiteAPI.Models
{
    public class info
    {
        [Key]
        public int recipe_id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string recipe_title { get; set; }

        public byte[]? recipe_image { get; set; } // Store image as byte array

        [Required]
        [MaxLength(50)]
        public required string create_by { get; set; }
    }
}

