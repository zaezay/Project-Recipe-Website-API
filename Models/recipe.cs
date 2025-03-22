using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecipeSharingWebsiteAPI.Models
{
    public class recipe
    {
        [Key]
        public int detail_id { get; set; }

        [Required]
        [ForeignKey("info")]
        public int recipe_id { get; set; } // Foreign Key to Info Table

        [Required]
        public string recipe_ingredient { get; set; }

        [Required]
        public string recipe_instruction { get; set; }

        public info? info { get; set; }
    }
}
