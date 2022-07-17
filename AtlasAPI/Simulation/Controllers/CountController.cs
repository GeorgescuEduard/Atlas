using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Simulation.Models;

namespace Simulation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountController : ControllerBase
    {
        private readonly DBContext _context;

        public CountController(DBContext context)
        {
            _context = context;
        }


        // GET: api/Count/5
        [Authorize]
        [HttpGet]
        public int GetFileCount()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            return _context.Issues.Count();

        }

        [Authorize]
        [HttpGet]
        [Route("CountVersion")]
        public int GetVersionsCount(string fileid)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            return _context.Versions.Count(w => w.IssueID == fileid);

        }

        [Authorize]
        [HttpGet]
        [Route("Type")]
        public int GetFileTypeCount(string filetype, string project)
        {
            return _context.Issues.Count(w => w.IssueType == filetype && w.Project == project);

        }

        [HttpGet]
        [Route("File")]
        public bool GetFileCount(string Name)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            if (_context.Issues.Count(w => w.IssueSummary == Name && w.UserId == userId) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        [HttpGet]
        [Route("Share")]
        public bool GetShareCount(string IdentityString)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            if (_context.Issues.Count(w => w.IssueID == IdentityString) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}