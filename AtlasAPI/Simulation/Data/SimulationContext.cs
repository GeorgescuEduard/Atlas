using Microsoft.EntityFrameworkCore;
using Simulation.Models;

namespace Simulation.Data
{
    public class SimulationContext : DbContext
    {
        public SimulationContext(DbContextOptions<SimulationContext> options)
            : base(options)
        {
        }
        public DbSet<Simulation.Models.Projects> Projects { get; set; }
    }
}
