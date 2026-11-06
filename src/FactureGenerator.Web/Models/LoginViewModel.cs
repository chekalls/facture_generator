using System.ComponentModel.DataAnnotations;

namespace FactureGenerator.Web.Models;

public class LoginViewModel
{
    [Display(Name = "Adresse e-mail")]
    [Required(ErrorMessage = "L'adresse e-mail est obligatoire")]
    [EmailAddress(ErrorMessage = "Format d'email invalide")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Mot de passe")]
    [Required(ErrorMessage = "Le mot de passe est obligatoire")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; } = false;
}