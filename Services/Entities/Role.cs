using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NATS.Services.Entities;

public class Role : IdentityRole<int>
{
    [Column("display_name")]
    [Required]
    [StringLength(50)]
    public string DisplayName { get; set; }

    // Navigation properties
    public virtual List<User> Users { get; set; }
}
