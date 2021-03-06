using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Simulation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Simulation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly ApplicationSettings _appSettings;

        public ApplicationUserController(UserManager<UserAccount> userManager, IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("Register")]
        //api/ApplicationUser/Register

        public async Task<Object> PostApplicationUser(UserAccountModel model)
        {
            var applicationUser = new UserAccount()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                VerificationCode = "CODE",
                ExpCode = new DateTime(2022, 8, 18),
                AssigneedProject = model.AssigneedProject,
                Rank = model.Rank,
                Access = model.Access
            };


            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);

                /*if (result.Succeeded)
                {
                    var message = new MimeMessage();

                    message.From.Add(new MailboxAddress("PasteCode", _appSettings.Sender));

                    message.To.Add(new MailboxAddress(model.UserName, model.Email));

                    message.Subject = "Your account has been created!";

                    message.Body = new TextPart("html")
                    {
                        Text = @"Hi, " + model.FirstName + model.LastName + ", <br><br>" +
                        "You account has been created and you can login now using this link. <br><br>" +
                        "<a href= \"" + _appSettings.ClientURL + "/user/login \">" + _appSettings.ClientURL + "/user/login</a><br><br>" +
                        "If you see this message and you haven't requested an account creation please report to support team."

                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect(_appSettings.SMTPServer, _appSettings.SMTPPort, false);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.AuthenticationMechanisms.Remove("NTLM");
                        client.Authenticate(_appSettings.Sender, _appSettings.Password);
                        client.Send(message);
                        client.Disconnect(true);
                    }
                }*/

                return Ok(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("UpdateDetails")]
        //api/ApplicationUser/UpdateDetails

        public async Task<Object> PutApplicationUserDetails(UserAccountModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);  

            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            try
            {
                var result = await _userManager.UpdateAsync(user);

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut]
        [Route("UpdatePassword")]
        //api/ApplicationUser/UpdatePassword

        public async Task<Object> PutApplicationUserPassword(UserAccountModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

            try
            {
                var result = await _userManager.UpdateAsync(user);

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("Login")]
        //api/ApplicationUser/Login

        public async Task<IActionResult> Login(LoginModel model)
        {

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {

                        new Claim("UserID", user.Id.ToString()),
                        new Claim("UserName", user.UserName.ToString()),
                        new Claim("FirstName", user.FirstName.ToString()),
                        new Claim("LastName", user.LastName.ToString()),
                        new Claim("AssigneedProject", user.AssigneedProject.ToString()),
                        new Claim("Rank",user.Rank.ToString()),
                        new Claim("Access", user.Access.ToString())

                    }),

                    Expires = DateTime.UtcNow.AddHours(8),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)

                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorect." });
            }

        }
    }
}