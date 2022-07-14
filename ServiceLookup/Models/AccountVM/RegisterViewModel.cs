using System.ComponentModel.DataAnnotations;

namespace ServiceLookup.Models.AccountVM
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Год рождения")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
