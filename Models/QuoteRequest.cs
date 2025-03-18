using System.ComponentModel.DataAnnotations;

namespace ProjektDT191G.Models
{

    public class QuoteRequest
    {
        // Properties
        public int QuoteRequestId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        public string? Phone { get; set; }

        [Required]
        [Display(Name = "Lecture Address")]
        public string? Address { get; set; }

        [Display(Name = "Message (optional)")]
        public string? Message { get; set; }

        [Display(Name = "Request date")]
        public DateTime RequestDate { get; set; } = DateTime.Now;

        // Om offerten är behandlad eller inte, false från start
        [Required]
        [Display(Name = "Processed")]
        public bool IsProcessed { get; set; } = false;

        // Koppling till föreläsning
        public int? LectureId { get; set; }
        public Lecture? Lecture { get; set; }

        // Koppling till föreläsare
        public int? SpeakerId { get; set; }
        public Speaker? Speaker { get; set; }

    }
}