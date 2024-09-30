using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Restaurante_API.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurante_API.Servicios
{
    public class AuthServicio
    {
        private readonly IConfiguration _configuration;
        private readonly ILogEmpleadoServicio _logEmpleadoServicio;
        private readonly IMapper _mapper;
        public AuthServicio(IConfiguration configuration, ILogEmpleadoServicio logEmpleadoServicio, IMapper mapper)
        {
            this._configuration = configuration;
            this._logEmpleadoServicio = logEmpleadoServicio;
            this._mapper = mapper;

        }

        public string CreateToken(LoginRequestDto usuario, string rol)
        {
            JwtSettings? jwtSettings = this._configuration.GetSection("JwtSettings").Get<JwtSettings>();



            Claim[] claims = new[]
            {

                new Claim(ClaimTypes.Name, usuario.usuario),
                new Claim("Password",usuario.password),
                new Claim(ClaimTypes.Role, rol) // Agrega el rol como un claim
                
            };
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            SigningCredentials key = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(jwtSettings.ExpirationInMinutes),
                Audience = jwtSettings.Audience,
                Issuer = jwtSettings.Issuer,
                SigningCredentials = key

            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(securityTokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
