using System.ComponentModel.DataAnnotations;

namespace ProjektDT191G.Models
{

    public class Lecture
    {
        // Properties
        public int LectureId { get; set; }

        [Required]
        [Display(Name = "Lecture Name")]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public int? Price { get; set; }

        // Koppling till offertförfrågningar
        public ICollection<QuoteRequest>? QuoteRequests { get; set; }

        // Koppling till kategori, FK
        public int? CategoryId { get; set; }

        // Navigering där varje föreläsning tillhör en kateori
        public Category? Category { get; set; }
    }
}