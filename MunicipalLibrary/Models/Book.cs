using System.ComponentModel.DataAnnotations;

namespace MunicipalLibrary.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "שם ספר")]
        public string? NameBook { get; set; }
        [Display(Name = "גובה ספר")]
        public int HeightBook { get; set; }
        public Shelf Shelf { get; set; }
    }
}
