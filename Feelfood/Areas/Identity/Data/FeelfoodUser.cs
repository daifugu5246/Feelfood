using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Feelfood.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Feelfood.Areas.Identity.Data;

// Add profile data for application users by adding properties to the FeelfoodUser class
public class FeelfoodUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string? FirstName { get; set; }
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string? LastName { get; set; }
    public int? JobId { get; set; }
    public JobModel? Job { get; set; }
    public ICollection<OrderModel>? Orders { get; set; }
}

