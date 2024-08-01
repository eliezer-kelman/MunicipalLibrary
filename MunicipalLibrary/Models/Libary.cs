using System.ComponentModel.DataAnnotations;

namespace MunicipalLibrary.Models
{
    public class Libary
    {
        [Key]
        public int Id { get; set; }
        public string? Genre { get; set; }

        public List<Shelf> ShelfList { get; set; } = new List<Shelf>();
    }
}
