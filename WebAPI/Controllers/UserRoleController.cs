using DataAccess.Interfaces;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin,Standart")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleRepository _roleRepository;

        public UserRoleController(IUserRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }


        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(string userID, string roleID)
        //{
        //    try
        //    {
        //        var role = await _roleRepository.getById(userID,roleID);

        //        if (role == null)
        //            return NotFound();

        //        return Ok(role);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Create(string userID, string roleID)
        {
            try
            {
               var result= await _roleRepository.Add(userID,roleID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(string userID, string roleID)
        {
            try
            {


                _roleRepository.Update(userID, roleID);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string userID, string roleID)
        {
            try
            {
 

                _roleRepository.Delete(userID,roleID);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
