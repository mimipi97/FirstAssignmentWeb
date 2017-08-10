﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    public class TimeLog
    {
        public int Id { get; set; }
        public int ParentTaskId { get; set; }
        public int ParentUserId { get; set; }
        public int TimeSpent { get; set; }
        public DateTime CreationDate { get; set; }
    }
}