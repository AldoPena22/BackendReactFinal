using System.ComponentModel.DataAnnotations;

namespace BackendReactFinal.Model.DTOs.Genre
{
    public class GenreCreateDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
