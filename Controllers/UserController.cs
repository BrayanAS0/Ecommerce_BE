using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("",Name ="GetUsers")]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            var userDto= _mapper.Map<List<UserDto>>(users);
            return Ok(userDto);
        }
        [HttpGet("{UserId:int}",Name ="GetUser")]
        public IActionResult GetUser(int UserId) {

            if (UserId <= 0) return BadRequest("El UserId no es valido");
            var user = _userRepository.GetUser(UserId);
            if (user == null) return NotFound("El usuario no fue encontrado");
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(user);
        }
        [HttpPost]
        public IActionResult CreateUser(UserRegisterDto userRegisterDto)
        {


        }
    }
}
