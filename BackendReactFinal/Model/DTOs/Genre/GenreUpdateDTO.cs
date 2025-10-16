using System.ComponentModel.DataAnnotations;

namespace BackendReactFinal.Model.DTOs.Genre
{
    public class GenreUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
