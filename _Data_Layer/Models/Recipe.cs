using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _Data_Layer.Models
{

    [Table("Recipe")]
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string Ingredients { get; set; } = string.Empty;
        public string Steps { get; set; } = string.Empty;

        [Column("CookingTime")]
        public string CookingTime { get; set; } = string.Empty;

        // Stored in database
        public string DietaryTagsJson { get; set; } = string.Empty;

        // Not mapped, for working in C# only
        [NotMapped]
        public List<string> DietaryTags
        {
            get => string.IsNullOrWhiteSpace(DietaryTagsJson)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(DietaryTagsJson) ?? new List<string>();

            set => DietaryTagsJson = JsonSerializer.Serialize(value ?? new List<string>());
        }

       // Optional helper for Razor forms (comma-separated)
        [NotMapped]
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
