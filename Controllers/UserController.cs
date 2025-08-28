using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace ApiEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController:ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("GetUsers", Name ="GetUsers")]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            var userDto= _mapper.Map<List<UserDto>>(users);
            return Ok(userDto);
        }
        [HttpGet("GetUser{UserId:int}", Name ="GetUser")]
        public IActionResult GetUser(int UserId) {

            if (UserId <= 0) return BadRequest("El UserId no es valido");
            var user = _userRepository.GetUser(UserId);
            if (user == null) return NotFound("El usuario no fue encontrado");
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(user);
        }
        [HttpPost("Register", Name ="Register")]
        [AllowAnonymous]
        public async  Task<ActionResult> CreateUser(CreateUserDto createUserDto)
        {
            
            if (createUserDto == null) return NoContent();
            if (string.IsNullOrWhiteSpace(createUserDto.Username)) return BadRequest($"Username invalido");
            if (!_userRepository.IsUniqueUser(createUserDto.Username)) return BadRequest($"El user {createUserDto.Username} ya existe");
            var user = await _userRepository.Register(createUserDto);
            if (user == null) return BadRequest("Algo salio mal al crear el usuario");
            return CreatedAtRoute("GetUser", new {id=user.Id});
        }

        [HttpPost("Login", Name ="Login")]
        [AllowAnonymous]

        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (userLoginDto == null) return BadRequest();
            if (string.IsNullOrWhiteSpace(userLoginDto.Username) || !ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userRepository.Login(userLoginDto);
            if (user == null) return Unauthorized();
            return Ok(user);

        }

    }
}
