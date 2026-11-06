using System.ComponentModel.DataAnnotations;

namespace FactureGenerator.Web.Models;

public class RegisterViewModel
{
    // user information section
    [Display(Name = "Adresse email")]
    [Required(ErrorMessage = "L'email est obligatoire pour l'inscription")]
    [EmailAddress(ErrorMessage = "Format d'email invalide")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Mot de passe")]
    [Required(ErrorMessage = "Le mot de passe est obligatoire pour l'inscription")]
    [DataType(DataType.Password)]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Confirmation du mot de passe")]
    [Required(ErrorMessage = "La confirmation du mot de passe est obligatoire pour l'inscription")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Display(Name = "Prénom")]
    [Required(ErrorMessage = "Le prénom est obligatoire")]
    [StringLength(100, ErrorMessage = "Le prénom ne peut pas dépasser 100 caractères")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Nom")]
    [Required(ErrorMessage = "Le nom est obligatoire")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string LastName { get; set; } = string.Empty;

    // company information section
    [Display(Name = "Nom de la société")]
    [Required(ErrorMessage = "La raison sociale de la société est obligatoire")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "La raison sociale doit contenir entre 2 et 200 caractères")]
    public string CompanyName { get; set; } = string.Empty;

    [Display(Name = "Adresse (facultatif)")]
    [StringLength(500, ErrorMessage = "L'adresse ne peut pas dépasser 500 caractères")]
    public string? CompanyAddress { get; set; }

    [Display(Name = "NIF (facultatif)")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Le NIF doit contenir exactement 10 chiffres")]
    public string? CompanyNif { get; set; }

    [Display(Name = "STAT (facultatif)")]
    [RegularExpression(@"^\d{17}$", ErrorMessage = "Le STAT doit contenir exactement 17 chiffres")]
    public string? CompanyStat { get; set; }
}