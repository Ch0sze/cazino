using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace cazino3.Areas.Identity.Data;

// Add profile data for application users by adding properties to the cazinoUser class
public class cazinoUser : IdentityUser
{

    [PersonalData]
    [Required]
    [Column(TypeName = "nvarchar(15)")]
    public string NickName { get; set; }

    [PersonalData]
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [PersonalData]
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }

    [PersonalData]
    [Required]
    [Column(TypeName = "nvarchar(9)")]
    public string PhoneNumber { get; set; }


    [PersonalData]
    [Required]
    [Column(TypeName = "DECIMAL(20, 0)DEFAULT 0")]
    public int WalletBalance { get; set; }
}

