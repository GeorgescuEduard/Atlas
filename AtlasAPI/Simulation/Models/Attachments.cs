using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simulation.Models
{
    public class Attachments
    {
        [Key]
        public int AttachmentID { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string AttachmentName { get; set; }

        [Column(TypeName = "varbinary(8000)")]
        public string AttachmentContent { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime AttachmentTime { get; set; }

        public string IssueID { get; set; }
        [ForeignKey("IssueID")]
        public virtual Issues Issue { get; set; }

    }
}
