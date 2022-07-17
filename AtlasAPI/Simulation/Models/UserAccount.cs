using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simulation.Models
{
    public class UserAccount : IdentityUser
    {
        [Column(TypeName = "nvarchar(30)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string VerificationCode { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ExpCode { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string AssigneedProject { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string Rank { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string Access { get; set; }

        public virtual ICollection<Issues> Issue { get; set; }

    }
}
