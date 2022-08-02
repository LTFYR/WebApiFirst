using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiFirst.DAL;
using WebApiFirst.Models;

namespace WebApiFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComputersController(AppDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            Computer computer = _context.Computers.FirstOrDefault(c => c.Id == id);
            if (computer == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(computer);
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetComputers()
        {
            List<Computer> computer = _context.Computers.ToList();
            return Ok(computer);
        }

        [HttpPost("create")]
        public IActionResult CreateComp(Computer computer)
        {
            if (computer == null) return NotFound();
            _context.Computers.Add(computer);
            _context.SaveChanges();
            return StatusCode(201, computer);
        }


        [HttpPut("edit/{id}")]
        public IActionResult Edit(int id, Computer computer)
        {
            if (id == 0) return NotFound();
            Computer current = _context.Computers.FirstOrDefault(c => c.Id == id);
            if(current is null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            current.Name = computer.Name;
            current.Brand = computer.Brand;
            current.Price = computer.Price;
            current.Quality = computer.Quality;
            current.Quantity = computer.Quantity;
            _context.SaveChanges();
            return NoContent();
        }


        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            Computer current = _context.Computers.FirstOrDefault(c => c.Id == id);
            if (current == null) return NotFound();
            _context.Computers.Remove(current);
            _context.SaveChanges();
            return Ok();
        }
    }
}
