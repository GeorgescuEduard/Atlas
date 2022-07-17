using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simulation.Models
{
    public class Versions
    {
        [Key]
        public int VersionID { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string VersionTag { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Project { get; set; }

        [Column(TypeName = "varchar(30)")]
        [Required]
        [MinLength(1)]
        public string IssueType { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        [Required]
        [MinLength(1)]
        public string IssueSummary { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueDescription { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string IssueStatus { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string IssueResolution { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueStepsToRepro { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string IssueSeverity { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string IssueProbability { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime IssueCreatedDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? IssueModifiedDate { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string ModifiedBy { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueFixVersion { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string IssueReporter { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string IssueAssignee { get; set; }

        public string IssueID { get; set; }
        [ForeignKey("IssueID")]
        public virtual Issues Issue { get; set; }
    }
}
