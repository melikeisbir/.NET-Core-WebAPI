using DataAccess.Interfaces;
using Entity;
using Entity.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly SignInManager<Kullanici> _signInManager;
        private readonly UserManager<Kullanici> _userManager;
        private readonly IConfiguration _configuration;

        public KullaniciController(IKullaniciRepository kullaniciRepository, SignInManager<Kullanici> signInManager, IConfiguration configuration, UserManager<Kullanici> userManager)
        {
            _kullaniciRepository = kullaniciRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var kullanici = await _kullaniciRepository.getAll();
                return Ok(kullanici);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var kullanici = await _kullaniciRepository.getById(id);

                if (kullanici == null)
                    return NotFound();

                return Ok(kullanici);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoginVM kullanici)
        {
            try
            {
               var rresult= await _kullaniciRepository.Add(kullanici);
                return Ok(rresult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Kullanici kullanici)
        {
            try
            {
             

                _kullaniciRepository.Update(kullanici);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVM kullanici)
        {
            var user = await _userManager.FindByNameAsync(kullanici.KullaniciAdi);

            if (user == null || !await _userManager.CheckPasswordAsync(user, kullanici.Sifre))
            {
                return Unauthorized(); // Yetkisiz erişim
            }

            var token = GenerateToken(user);

            return Ok(new { Token = token });
        }

        private string GenerateToken(IdentityUser user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var kullanici = _kullaniciRepository.getById(id).Result;

                if (kullanici == null)
                    return NotFound();

                _kullaniciRepository.Delete(kullanici.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
