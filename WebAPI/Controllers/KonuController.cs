using DataAccess.Interfaces;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Standart")]
    public class KonuController : ControllerBase
    {
        private readonly IKonuRepository _konuRepository;

        public KonuController(IKonuRepository konuRepository)
        {
            _konuRepository = konuRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var konular = await _konuRepository.getAll();
                return Ok(konular);
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
                var konu = await _konuRepository.getById(id);

                if (konu == null)
                    return NotFound();

                return Ok(konu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Konu konu)
        {
            try
            {
                await _konuRepository.Add(konu);
                return CreatedAtAction(nameof(GetById), new { id = konu.Id }, konu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Konu konu)
        {
            try
            {
                if (id != konu.Id)
                    return BadRequest();

                _konuRepository.Update(konu);
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
                var konu = _konuRepository.getById(id).Result;

                if (konu == null)
                    return NotFound();

                _konuRepository.Delete(konu.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
