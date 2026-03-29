using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentRepositoryGit.Models;
using StudentRepositoryGit.Repositories;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace StudentRepositoryGit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly StudentsRepository _repo;

        public StudentsController(StudentsRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<CatsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
       // [Authorize(Roles = "Admin")]

        public ActionResult<IEnumerable<Student>> GetAll([FromQuery] int minAge, [FromQuery] int? maxAge, [FromQuery] string? nameFilter)
        {
            try
            {
                IEnumerable<Student> result = _repo.Get(minAge, maxAge, nameFilter);
                if (result == null || result.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(result);
            }
                catch (ArgumentException ex)
                {
                return BadRequest(ex.Message);
                }
        }


        // GET api/<StudentController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public ActionResult<Student> Get(int id)
        {
            Student? cat = _repo.GetById(id);
            if (cat == null)
            {
                return NotFound();
            }
            return Ok(cat);
        }

        // POST api/<StudentsController>
        [HttpPost]
        public Student Post([FromBody] Student newStudent)
        {
            return _repo.Add(newStudent);
        }
        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public Student? Put(int id, [FromBody] Student value)
        {
            return _repo.Update(id, value);
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public Student? Delete(int id)
        {
            return _repo.Delete(id);
        }
        [HttpOptions]
        public void Options()
        {
        }
    }
}
