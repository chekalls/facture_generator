using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FactureGenerator.Models;

[Table("Companies")]
public class Company
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Column(TypeName = "varchar(11)")]
    public string Nif {get;set;}
    [Column(TypeName = "varchar(11)")]
    public string Stat {get;set;}

    [Column(TypeName = "varchar(100)")]
    public string Address {get;set;}
    [Column(TypeName = "varchar(255)")]
    public string Designation {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
}

