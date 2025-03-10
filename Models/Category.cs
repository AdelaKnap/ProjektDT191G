using System.ComponentModel.DataAnnotations;

namespace ProjektDT191G.Models
{

    public class Category
    {
        // Properties
        public int CategoryId { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        // Lista med föreläsningar, en kategori kan innehålla flera föreläsningar
        public ICollection<Lecture>? Lectures { get; set; }
    }

}