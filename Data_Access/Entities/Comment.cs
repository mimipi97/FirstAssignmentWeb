using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int ParentTaskId { get; set; }
        public int ParentUserId { get; set; }
        public string CommentBody { get; set; }
    }
}