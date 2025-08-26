using ApiEcommerce.Data;
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiEcommerce.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly string _secretKey;
        public UserRepository(IMapper mapper, ApplicationDbContext dbContext,IConfiguration configuration) {
            _dbContext = dbContext;
            _secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
        _mapper = mapper;

        }
     public   User? GetUser(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

  public      ICollection<User>GetUsers()
        {
            return _dbContext.Users.ToList();
        }

      public  bool IsUniqueUser(string userName)
        {
            return !_dbContext.Users.Any(x=> x.Username.Trim().ToLower()== userName.Trim().ToLower());
        }

       public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            UserLoginResponseDto userLoginResponseDto = new UserLoginResponseDto();
            if (string.IsNullOrEmpty(userLoginDto.Username))
            {
                userLoginResponseDto.Token = "";
                userLoginResponseDto.Message = "EL user Name es requerido";
                userLoginResponseDto.User =null;
                return userLoginResponseDto;
            }
            var user = await _dbContext.Users
                .FirstOrDefaultAsync<User>
                (x => x.Username.ToLower().Trim() == userLoginDto.Username.ToLower().Trim());
            if (user == null)
            {
                userLoginResponseDto.Token = "";
                userLoginResponseDto.Message = "User no encontrado";
                userLoginResponseDto.User = null;
                return userLoginResponseDto;
            }
            if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password , user.Password))
            {
                userLoginResponseDto.Token = "";
                userLoginResponseDto.Message = "Contraseña Incorrecta";
                userLoginResponseDto.User = null;
                return userLoginResponseDto;
            }

            var handlerToken = new JwtSecurityTokenHandler();
            if (string.IsNullOrEmpty(_secretKey)) throw new InvalidOperationException("SecretKey no esta configurada");
            var key = Encoding.UTF8.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor {
            Subject= new ClaimsIdentity(new [] { 
            new Claim("id",user.Id.ToString()),
            new Claim ("usernamae",user.Username),
            new Claim(ClaimTypes.Role,user.Role ??string.Empty)
            }),
            Expires =DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token =handlerToken.CreateToken(tokenDescriptor);
            userLoginResponseDto.Token = handlerToken.WriteToken(token);
            userLoginResponseDto.User = new UserRegisterDto()
            {
                UserName = user.Username,
                Name = user.Name,
                Role = user.Role,
                Password=user.Password ??""
            };
            userLoginResponseDto.Message = "Usuario loggeado correctamente";

            return userLoginResponseDto;
        }

    public     async Task<User> Register(CreateUserDto createUserDto)
        {
            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
            createUserDto.Username = string.IsNullOrEmpty(createUserDto.Username) ? "No Username" : createUserDto.Username;
            createUserDto.Password = encryptedPassword;
            var user = _mapper.Map<User>(createUserDto);

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;




        }
    }
}
