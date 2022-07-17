using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simulation.Models
{
    public class Activity
    {
        [Key]
        public int ActivityID { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string ActivityText { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string ProjectOldValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string ProjectNewValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueTypeOldValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueTypeNewValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueSummaryOldValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueSummaryNewValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueDescriptionOldValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueDescriptionNewValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueStatusOldValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueStatusNewValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueResolutionOldValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueResolutionNewValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueStepsToReproOldValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueStepsToReproNewValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueSeverityOldValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueSeverityNewValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueProbabilityOldValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueProbabilityNewValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueFixVersionOldValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueFixVersionNewValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueReporterOldValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueReporterNewValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueAssigneeOldValue { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string IssueAssigneeNewValue { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string ActivityUser { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ActivityTime { get; set; }

        public string IssueID { get; set; }
        [ForeignKey("IssueID")]
        public virtual Issues Issue { get; set; }
    }
}
