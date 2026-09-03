using System.ComponentModel.DataAnnotations;

namespace Core_Proje.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Lütfen rol adı giriniz!")]
        public string RoleName { get; set; }
    }
}
