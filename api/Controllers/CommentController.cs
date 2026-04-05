using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Models;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            return BadRequest(ModelState);

            var comments = await _commentRepository.GetAllAsync();
            var commentDTO = comments.Select(s=> s.ToCommentDTO());
            return Ok(commentDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            return BadRequest(ModelState);

            var comment = await _commentRepository.GetByIdAsync(id);

            if(comment == null)
            {
              return NotFound();   
            }

            return Ok(comment.ToCommentDTO());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId,[FromBody] CreateCommentDTO createCommentDTO)
        {
            if(!ModelState.IsValid)
            return BadRequest(ModelState);

            var stockModel = await _stockRepository.StockExist(stockId);

            if(!stockModel)
            {
                BadRequest("Stock does not exist!");    
            }

            var comment = createCommentDTO.ToCommentFromCreateDTO(stockId);

            await _commentRepository.CreateAsync(comment);

            return CreatedAtAction(nameof(GetById), new {id = comment.Id}, comment.ToCommentDTO());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDTO updateCommentModel)
        {
            if(!ModelState.IsValid)
            return BadRequest(ModelState);

            var commentModel = await _commentRepository.UpdateAsync(id, updateCommentModel.ToCommentFromUpdateCommentDTO());

            if(commentModel == null)
            {
                return NotFound("Comment does not exist!");                
            }

            return Ok(commentModel.ToCommentDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid)
            return BadRequest(ModelState);

            var comment = await _commentRepository.DeleteAsync(id);

            if(comment == null)
            {
                return NotFound("Comment does not exist!");
            }

            return Ok(comment);
        }
    }
}