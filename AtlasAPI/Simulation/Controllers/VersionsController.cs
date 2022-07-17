using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulation.Models;

namespace Simulation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionsController : ControllerBase
    {
        private readonly DBContext _context;

        public VersionsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Versions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Versions>>> GetVersions(string version)
        {
            return await _context.Versions.Where(t => t.IssueID == version).OrderByDescending(a => a.IssueModifiedDate).ToListAsync();
        }

        // GET: api/Versions/5
        [HttpGet("{version}")]
        public async Task<ActionResult<IEnumerable<Versions>>> GetVersion(string version)
        {
            return await _context.Versions.Where(t => t.IssueID == version).OrderByDescending(a => a.IssueModifiedDate).ToListAsync();
        }

        [HttpGet]
        [Route("Select")]
        public ActionResult<Versions> GetSelectVersion(int file)
        {
            return _context.Versions.FirstOrDefault(t => t.VersionID == file);

        }

        // PUT: api/Versions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVersions(int id, Versions versions)
        {
            if (id != versions.VersionID)
            {
                return BadRequest();
            }

            _context.Entry(versions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VersionsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Versions
        [HttpPost]
        public async Task<ActionResult<Versions>> PostVersions(Versions versions)
        {
            _context.Versions.Add(versions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVersions", new { id = versions.VersionID }, versions);
        }

        // DELETE: api/Versions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Versions>> DeleteVersions(int id)
        {
            var versions = await _context.Versions.FindAsync(id);
            if (versions == null)
            {
                return NotFound();
            }

            _context.Versions.Remove(versions);
            await _context.SaveChangesAsync();

            return versions;
        }

        private bool VersionsExists(int id)
        {
            return _context.Versions.Any(e => e.VersionID == id);
        }
    }
}
