﻿using MuzON.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzON.BLL.DTO
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string CommentText { get; set; }
        public Guid? SongId { get; set; }
        public Guid? UserId { get; set; }

        public virtual Song Song { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
