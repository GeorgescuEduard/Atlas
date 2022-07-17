using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simulation.Models
{
    public class Comments
    {
        [Key]
        public int CommentID { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string CommentText { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string CommentUser { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CommentTime { get; set; }

        public string IssueID { get; set; }
        [ForeignKey("IssueID")]
        public virtual Issues Issue { get; set; }
    }
}
