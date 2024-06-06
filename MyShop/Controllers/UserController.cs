using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;
        private ILogger<UserController> _logger;
        public UserController(IUserServices userServices, IMapper mapper, ILogger<UserController> logger)
        {
            _userServices = userServices;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] UserDtoRegisterAndUpdate user)
        {
            try
            {
                User per = _mapper.Map<UserDtoRegisterAndUpdate, User>(user);
                User usr = await _userServices.AddUser(per);
                UserDtoRegisterAndUpdate userGetDTOs = _mapper.Map<User, UserDtoRegisterAndUpdate>(usr);
                if (userGetDTOs == null)
                {
                    return BadRequest();
                }
                return Ok(userGetDTOs);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("password")]
        public ActionResult<int> Password([FromBody] string pass)
        {
            try
            {
                if (string.IsNullOrEmpty(pass))
                {
                    return NoContent();
                }
                return _userServices.CheckPassword(pass);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDtoLogin>> Login([FromBody] UserDtoLogin userDtoLogin)
        {
            try
            {
                var user = _mapper.Map<User>(userDtoLogin);
                var usr = await _userServices.Login(user.Email, user.Password);
                _logger.LogInformation($"Login attempted with User Name,{user.Email} and password{user.Password} ");

                var userGetDTOs = _mapper.Map<UserDtoGet>(usr);
                if (userGetDTOs == null)
                {
                    return Unauthorized();
                }
                return Ok(userGetDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

     

        [HttpPut]
        public async Task<ActionResult<User>> Update([FromBody] User userToUpdate)
        {
            try
            {

                var updatedUser = await _userServices.UpdateUser(userToUpdate);
                
                if (updatedUser == null)
                {
                    return NoContent();
                }

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
