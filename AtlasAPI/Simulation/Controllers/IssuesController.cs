using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {

        private readonly DBContext _context;

        public IssuesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/File
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Issues>>> GetIssuesDetail(int skip, int take, string project)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            return await _context.Issues.Where(t => t.Project == project)
                .OrderByDescending(t => t.IssueCreatedDate).Skip(skip).Take(take).Select(t => new Issues()
                {
                    IssueID = t.IssueID,
                    IssueType = t.IssueType,
                    IssueSummary = t.IssueSummary,
                    IssueDescription = t.IssueDescription,
                    IssueStatus = t.IssueStatus,
                    IssueResolution = t.IssueResolution,
                    IssueStepsToRepro = t.IssueStepsToRepro,
                    IssueSeverity = t.IssueSeverity,
                    IssueProbability = t.IssueProbability,
                    IssueCreatedDate = t.IssueCreatedDate,
                    IssueModifiedDate = t.IssueModifiedDate,
                    IssueFixVersion = t.IssueFixVersion,
                    IssueReporter = t.IssueReporter,
                    IssueAssignee = t.IssueAssignee

                }).ToListAsync();

        }

        [HttpGet]
        [Route("Reporter")]
        public async Task<ActionResult<IEnumerable<Issues>>> GetReporterIssuesDetail(int skip, int take, string reporter, string project)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            return await _context.Issues.Where(t => t.Project == project)
                .OrderByDescending(t => t.IssueCreatedDate).Where(t => t.IssueReporter == reporter).Skip(skip).Take(take).Select(t => new Issues()
                {
                    IssueID = t.IssueID,
                    IssueType = t.IssueType,
                    IssueSummary = t.IssueSummary,
                    IssueDescription = t.IssueDescription,
                    IssueStatus = t.IssueStatus,
                    IssueResolution = t.IssueResolution,
                    IssueStepsToRepro = t.IssueStepsToRepro,
                    IssueSeverity = t.IssueSeverity,
                    IssueProbability = t.IssueProbability,
                    IssueCreatedDate = t.IssueCreatedDate,
                    IssueModifiedDate = t.IssueModifiedDate,
                    IssueFixVersion = t.IssueFixVersion,
                    IssueReporter = t.IssueReporter,
                    IssueAssignee = t.IssueAssignee

                }).ToListAsync();

        }

        [HttpGet]
        [Route("Assignee")]
        public async Task<ActionResult<IEnumerable<Issues>>> GetAssigneeIssuesDetail(int skip, int take, string assignee, string project)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            return await _context.Issues.Where(t => t.Project == project)
                .OrderByDescending(t => t.IssueCreatedDate).Where(t => t.IssueAssignee == assignee).Skip(skip).Take(take).Select(t => new Issues()
                {
                    IssueID = t.IssueID,
                    IssueType = t.IssueType,
                    IssueSummary = t.IssueSummary,
                    IssueDescription = t.IssueDescription,
                    IssueStatus = t.IssueStatus,
                    IssueResolution = t.IssueResolution,
                    IssueStepsToRepro = t.IssueStepsToRepro,
                    IssueSeverity = t.IssueSeverity,
                    IssueProbability = t.IssueProbability,
                    IssueCreatedDate = t.IssueCreatedDate,
                    IssueModifiedDate = t.IssueModifiedDate,
                    IssueFixVersion = t.IssueFixVersion,
                    IssueReporter = t.IssueReporter,
                    IssueAssignee = t.IssueAssignee

                }).ToListAsync();

        }

        [HttpGet("{id}")]
        public ActionResult<Issues> GetShare(string id)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var file = _context.Issues.FirstOrDefault(t => t.IssueID == id);

            /*if (file == null)
            {
                return NotFound();
            }

            if (userId != file.UserId)
            {
                return NotFound();
            }*/

            return file;
        }

        [HttpGet]
        [Route("Content")]
        public string GetFileContent(string fileid)
        {

            return _context.Issues.Where(t => t.IssueID == fileid).ToString();

        }

        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult<IEnumerable<Issues>>> SearchAction(string search, string project)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            return await _context.Issues.Where(t => (t.IssueSummary.Contains(search) || t.IssueDescription.Contains(search) || t.IssueReporter.Contains(search) || t.IssueAssignee.Contains(search)) && t.Project == project).ToListAsync();

        }

        // PUT: api/File/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFile(string id, Issues file)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            file.UserId = userId;

            if (id != file.IssueID)
            {
                return BadRequest();
            }

            _context.Entry(file).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileExists(id))
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

        // POST: api/File

        [HttpPost]
        public async Task<ActionResult<Issues>> PostFile(Issues file)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            file.UserId = userId;
            file.IssueCreatedDate = DateTime.Now;
            _context.Issues.Add(file);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/File/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Issues>> DeleteFile(string id)
        {
            var file = await _context.Issues.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            _context.Issues.Remove(file);
            await _context.SaveChangesAsync();

            return file;
        }

        private bool FileExists(string id)
        {
            return _context.Issues.Any(e => e.IssueID == id);
        }

    }
}
