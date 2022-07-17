using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simulation.Models
{
    public class Projects
    {
        [Key]

        [Column(TypeName = "varchar(30)")]
        public string ProjectKEY { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string ProjectText { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ProjectTime { get; set; }

    }
}
