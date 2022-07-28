using System.ComponentModel.DataAnnotations;

namespace ServiceLookup.Models.AccountVM
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Имя")]
        public string? Name { get; set; } 

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Фамилия")]
        public string? Surname { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Required]
        [Display(Name = "Login")]
        public string? UserName { get; set; }

        [Required]
        [Range(typeof(DateTime), "01-01-1950", "01-01-2004")/*, ErrorMessage = "Введіть коректну дату народження"*/]
        [Display(Name = "Год рождения")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string? PasswordConfirm { get; set; }
    }
}
