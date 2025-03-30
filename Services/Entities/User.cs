﻿using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NATS.Services.Entities;

public class User : IdentityUser<int>
{
    // Navigation properties
    public virtual List<Role> Roles { get; set; }
    public virtual List<Post> Posts { get; set; }

    //Navigation properties for convinience
    [NotMapped]
    public virtual Role Role => Roles.SingleOrDefault();
}