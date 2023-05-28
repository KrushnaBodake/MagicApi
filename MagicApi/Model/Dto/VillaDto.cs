using System.ComponentModel.DataAnnotations;

namespace MagicApi.Model.Dto
{
    public class VillaDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string? Name { get; set; }

        public DateTime  CreateDate { get; set; }
    }
}
