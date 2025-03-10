using System.ComponentModel.DataAnnotations;

namespace ProjektDT191G.Models
{

    public class Speaker
    {
        // properties
        public int SpeakerId { get; set; }

        [Required]
        public string? Name { get; set; }

        // En föreläsare kan vara kopplad till flera offerter
        public ICollection<QuoteRequest>? QuoteRequests { get; set; }
    }
}