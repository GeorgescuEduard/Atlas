using System.ComponentModel.DataAnnotations;

namespace Simulation.Models
{
    public class UserAccountModel
    {

        [MinLength(6)]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9._]*[a-zA-Z0-9]$")]
        public string UserName { get; set; }

        [MinLength(2)]
        public string FirstName { get; set; }

        [MinLength(2)]
        public string LastName { get; set; }

        [EmailAddress]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9._@]*[a-zA-Z]$")]
        public string Email { get; set; }

        [MinLength(10)]
        [MaxLength(16)]
        [RegularExpression("^[0-9]*$")]
        public string PhoneNumber { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        public string AssigneedProject { get; set; }

        [MinLength(2)]
        public string Rank { get; set; }

        [MinLength(2)]
        public string Access { get; set; }
    }
}
