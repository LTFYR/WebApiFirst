using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiFirst.DAL;
using WebApiFirst.DTOs;
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
        public IActionResult CreateComp(ComputerPostDto computerPostDto)
        {
            if (computerPostDto == null) return NotFound();
            Computer computer = new Computer
            {
                Brand = computerPostDto.Brand,
                Name = computerPostDto.Name,
                Price = computerPostDto.Price,
                Quality = computerPostDto.Quality,
                Quantity = computerPostDto.Quantity,
            };
            _context.Computers.Add(computer);
            _context.SaveChanges();
            return StatusCode(201, computer);
        }


        [HttpPut("edit/{id}")]
        public IActionResult Edit(int id, ComputerPostDto computerPostDto)
        {
            if (id == 0) return NotFound();
            Computer current = _context.Computers.FirstOrDefault(c => c.Id == id);
            if(current is null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _context.Entry(current).CurrentValues.SetValues(computerPostDto);
            //current.Name = computer.Name;
            //current.Brand = computer.Brand;
            //current.Price = computer.Price;
            //current.Quality = computer.Quality;
            //current.Quantity = computer.Quantity;
            _context.SaveChanges();
            return StatusCode(200,computerPostDto);
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
