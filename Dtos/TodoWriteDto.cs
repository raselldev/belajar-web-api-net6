using System.ComponentModel.DataAnnotations;

namespace BelajarWebApi.Dtos
{
    public class TodoWriteDto
    {
        [Required(ErrorMessage = "Judul Tidak Boleh Kosong")]
        public string? Title { get; set; }
        public string? Note { get; set; }
    }
}
