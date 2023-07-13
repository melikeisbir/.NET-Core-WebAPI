using DataAccess.Interfaces;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]

    public class DersController : ControllerBase
    {
        private readonly IDersRepository _dersRepository;

        public DersController(IDersRepository dersRepository)
        {
            _dersRepository = dersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var dersler = await _dersRepository.getAll();
                return Ok(dersler);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var ders = await _dersRepository.getById(id);

                if (ders == null)
                    return NotFound();

                return Ok(ders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ders ders)
        {
            try
            {
                await _dersRepository.Add(ders);
                return CreatedAtAction(nameof(GetById), new { id = ders.Id }, ders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Ders ders)
        {
            try
            {
                if (id != ders.Id)
                    return BadRequest();

                _dersRepository.Update(ders);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ders = _dersRepository.getById(id).Result;

                if (ders == null)
                    return NotFound();

                _dersRepository.Delete(ders.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
