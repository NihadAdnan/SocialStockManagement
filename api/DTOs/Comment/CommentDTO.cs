using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Comment
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
    }
}