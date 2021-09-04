using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProgrammersBlog.Entities.Dtos
{
    public class CategoryAddDto
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden fazla olmamalıdır.")]
        [MinLength(4, ErrorMessage = "{0} {1} karakterden az olmamalıdır.")]
        public string Name { get; set; }


        [DisplayName("Kategori Açıklaması")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden fazla olmamalıdır.")]
        [MinLength(4, ErrorMessage = "{0} {1} karakterden az olmamalıdır.")]
        public string Description { get; set; }


        [DisplayName("Kategori Not Alanı")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden fazla    olmamalıdır.")]
        [MinLength(4, ErrorMessage = "{0} {1} karakterden az olmamalıdır.")]


        public string Note { get; set; }
        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]


        public bool IsActive { get; set; }
    }
}
