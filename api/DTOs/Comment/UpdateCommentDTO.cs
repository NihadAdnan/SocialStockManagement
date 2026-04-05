using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Comment
{
    public class UpdateCommentDTO
    {
        [Required]
        [MinLength(5,ErrorMessage = "Title must be more then 5 characters!")]
        [MaxLength(280,ErrorMessage = "Title can not be more then 280 characters!")]
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
    }
}