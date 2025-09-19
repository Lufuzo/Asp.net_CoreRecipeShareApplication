using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace RecipeShareApplication.ModelViews
{
    public class RecipeModelView
    {

        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        public string Ingredients { get; set; } = string.Empty;
        public string Steps { get; set; } = string.Empty;
        [Column("CookingTime")]
        public string CookingTime { get; set; }
        // Dietary tags stored as JSON string in the database via EF Core ValueConverter.
        public List<string> DietaryTags { get; set; } = new List<string>();

        // Helper property for Razor form binding
        public string DietaryTagsString
        {
            get => string.Join(", ", DietaryTags);
            set => DietaryTags = string.IsNullOrWhiteSpace(value)
                ? new List<string>()
                : value.Split(',', StringSplitOptions.RemoveEmptyEntries)
                       .Select(t => t.Trim())
                       .ToList();
        }
    
    }
}
