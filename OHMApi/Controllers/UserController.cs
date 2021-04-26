using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OHMApi.Data;
using OHMApi.Models;
using OHMDataManager.Library.DataAccess;
using OHMDataManager.Library.Models;

namespace OHMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserData _data;
        private readonly ILogger<UserController> _logger;

        public UserController(ApplicationDbContext context,
                              UserManager<IdentityUser> userManager,
                              IUserData data,
                              ILogger<UserController> logger)
        {
            _context = context;
            _userManager = userManager;
            _data = data;
            _logger = logger;
        }


        [HttpGet]
        public UserModel GetById()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return _data.GetUserById(userId);
        }


        public class UserRegistrationModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }

            public UserRegistrationModel(string firstName, string lastName, string email, string password)
            {
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                Password = password;
            }
        }



        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegistrationModel user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser is null)
                {
                    IdentityUser newUser = new IdentityUser
                    {
                        Email = user.Email,
                        EmailConfirmed = true,
                        UserName = user.Email
                    };

                    IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
                    
                    if (result.Succeeded)
                    {
                        existingUser = await _userManager.FindByEmailAsync(user.Email);
                        if (existingUser is null)
                        {
                            return BadRequest();
                        }

                        UserModel u = new UserModel{ 
                            Id = existingUser.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email
                        };

                        _data.CreateUser(u);
                        return Ok();
                    }
                }
            }

            return BadRequest();
        }


        


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllUsers")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            List<ApplicationUserModel> output = new List<ApplicationUserModel>();
                     
            var users = _context.Users.ToList();
            var userRoles = from ur in _context.UserRoles
                            join r in _context.Roles on ur.RoleId equals r.Id
                            select new { ur.UserId, ur.RoleId, r.Name };

            foreach (var user in users)
            {
                ApplicationUserModel userM = new ApplicationUserModel
                {
                    Id = user.Id,
                    Email = user.Email
                };

                userM.Roles = userRoles.Where(x => x.UserId == userM.Id).ToDictionary(key => key.RoleId, value => value.Name);

                output.Add(userM);
            }
            
            return output;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles()
        {
            
            var roles = _context.Roles.ToDictionary(x => x.Id, x => x.Name);

            return roles;
            
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/AddRole")]
        public async Task AddRole(UserRolePairModel pair)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(pair.UserId);

            _logger.LogInformation("Admin {Admin} added user {User} to role {Role}",
                loggedInUserId, user.Id, pair.RoleName);

            await _userManager.AddToRoleAsync(user, pair.RoleName);
            
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/RemoveRole")]
        public async Task RemoveRole(UserRolePairModel pair)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(pair.UserId);

            _logger.LogInformation("Admin {Admin} removed user {User} from role {Role}",
                loggedInUserId, user.Id, pair.RoleName);

            await _userManager.RemoveFromRoleAsync(user, pair.RoleName);
            
        }
    }
}