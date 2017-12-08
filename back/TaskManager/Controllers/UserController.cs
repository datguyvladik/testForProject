using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Models.DataAccess;
using TaskManager.Core.Models.DTO;
using TaskManager.Properties;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<UserDbModel> _userManager;
        private readonly SignInManager<UserDbModel> _signInManager;

        public UserController(UserManager<UserDbModel> userManager, SignInManager<UserDbModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterNewUser([FromBody]UserDto registrationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = Mapper.Map<UserDto, UserDbModel>(registrationData);

            var result = await _userManager.CreateAsync(newUser, registrationData.Password);

            var userId = (await _userManager.FindByNameAsync(registrationData.Email)).Id;

            if (!result.Succeeded)
                return BadRequest();

            return Ok(userId);
        }

        [HttpGet]
        [Route("login/{userName}/{password}")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest(Resources.InvalidUserName);
            }
            var check = await _userManager.CheckPasswordAsync(user, password);
            if (!check)
            {
                return BadRequest(Resources.InvalidPass);
            }
            await _signInManager.SignInAsync(user, false);
            return Ok(user.Id);
        }
    }
}
