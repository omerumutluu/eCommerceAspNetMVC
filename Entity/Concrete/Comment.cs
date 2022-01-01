using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public float CommentLevel { get; set; }
        [StringLength(100)]
        public string CommentTitle { get; set; }
        public string CommentDetail { get; set; }
    }
}
