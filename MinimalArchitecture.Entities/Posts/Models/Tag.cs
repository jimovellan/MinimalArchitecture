﻿using MinimalArchitecture.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Entities.Posts.Models
{
    public class Tag : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
