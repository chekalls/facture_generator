using Microsoft.AspNetCore.Identity;

namespace FactureGenerator.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName {get;set;}
    public string LastName {get;set;}
    public string Email {get;set;}
}