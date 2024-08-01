using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MunicipalLibrary.Models
{
    public class Shelf
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "מספר מדף")]
        public int NumShelf { get; set; }
        [Display(Name = "גובה מדף")]
        public int HeightShelf { get; set; }
        public Libary Library { get; set; }

        public List<Book> BookList { get; set; } = new List<Book>();


    }

    
}
