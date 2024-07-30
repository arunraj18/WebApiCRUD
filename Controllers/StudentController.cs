using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDopr.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDopr.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        public StudentController(StudentContext _dbContext)
        {
            this._dbContext = _dbContext;

        }

        private readonly StudentContext _dbContext;
        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            if(_dbContext.Students == null)
            {
                return NotFound();
            }
            return await _dbContext.Students.ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetById(int id)
        {
            if (_dbContext.Students == null)
            {
                return NotFound();
            }
            var student = await _dbContext.Students.FindAsync(id);
            if(student == null)
            {
                return NotFound();
            }

            return student;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Student>> StudentEntry(Student student)
        {
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { ID = student.Id }, student);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> StudentUpdate(int id, [FromBody]Student student)
        {
            var entry = await _dbContext.Students.FindAsync(id);
            if(entry != null)
            {
                entry.Name = student.Name;
                entry.RollNo = student.RollNo;
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { ID = student.Id }, student);
            }
            return NotFound();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var entry = await _dbContext.Students.FindAsync(id);
            if (entry != null)
            {
                _dbContext.Remove(entry);
                await _dbContext.SaveChangesAsync();


            }
           
        }
    }
}

