using System;

namespace Data_Access.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public int AssigneeId { get; set; }
        public int Grade { get; set; }
        public DateTime CreationDate { get; set; }//might set up problems
        public DateTime RecentDate { get; set; }
        public string Status { get; set; }
    }
}