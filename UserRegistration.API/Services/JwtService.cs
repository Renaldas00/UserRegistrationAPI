using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserRegistration.BLL.Interfaces;
using UserRegistration.DAL.Entities;

namespace UserRegistration.BLL.Services
{
    public class JwtService : IJwtService
    {
        //private readonly IConfiguration _configuration;

        //public JwtService(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        //public string GetJwtToken(Guid userId, string userName, string role)
        //{
        //    var secretKey = _configuration.GetSection("Jwt:Key").Value;
        //    var issuer = _configuration.GetSection("Jwt:Issuer").Value;
        //    var audience = _configuration.GetSection("Jwt:Audience").Value;

        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        //        new Claim(ClaimTypes.Name, userName),
        //        new Claim(ClaimTypes.Role, role)
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        //    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var token = new JwtSecurityToken(
        //        issuer: issuer,
        //        audience: audience,
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(1),
        //        signingCredentials: cred);

        //    return new JwtSecurityTokenHandler().WriteToken(token);

        //}
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        public JwtService(IConfiguration conf)
        {
            _secretKey = conf.GetValue<string>("Jwt:Key") ?? "";
            _issuer = conf.GetSection("Jwt:Issuer").Value ?? "";
            _audience = conf.GetSection("Jwt:Audience").Value ?? "";
        }
        public string GetJwtToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new (ClaimTypes.NameIdentifier, account.Id.ToString()),
                new (ClaimTypes.Name, account.UserName.ToString()),
                new (ClaimTypes.Role, account.Role),
                new (ClaimTypes.Email, account.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _issuer,
                Audience = _audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
