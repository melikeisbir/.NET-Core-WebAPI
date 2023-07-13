using DataAccess.Interfaces;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Standart")]
    public class SoruController : ControllerBase
    {
        private readonly ISoruRepository _soruRepository;

        public SoruController(ISoruRepository soruRepository)
        {
            _soruRepository = soruRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sorular = await _soruRepository.getAll();
                return Ok(sorular);
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
                var soru = await _soruRepository.getById(id);

                if (soru == null)
                    return NotFound();

                return Ok(soru);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Soru soru)
        {
            try
            {
                await _soruRepository.Add(soru);
                return CreatedAtAction(nameof(GetById), new { id = soru.Id }, soru);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Soru soru)
        {
            try
            {
                if (id != soru.Id)
                    return BadRequest();

                _soruRepository.Update(soru);
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
                var soru = _soruRepository.getById(id).Result;

                if (soru == null)
                    return NotFound();

                _soruRepository.Delete(soru.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
