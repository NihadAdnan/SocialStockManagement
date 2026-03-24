using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList().Select(s => s.ToStockDTO());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDTO createStockDTO)
        {
            var stockDTO = createStockDTO.ToStockFromCreateStockDTO();
            _context.Stocks.Add(stockDTO);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockDTO.Id }, stockDTO.ToStockDTO());
        }
    }
}