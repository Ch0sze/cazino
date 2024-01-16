using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace cazino3.Areas.Identity.Data;

// Add profile data for application users by adding properties to the cazinoUser class
public class cashIn
{

    [PersonalData]
    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string UserEmail { get; set; }

    [PersonalData]
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Id { get; set; }

    [PersonalData]
    [Required]
    [Column(TypeName = "DECIMAL(20, 0)DEFAULT 0")]
    public int Amount { get; set; }

    [PersonalData]
    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public DateTime Date { get; set; }


}

