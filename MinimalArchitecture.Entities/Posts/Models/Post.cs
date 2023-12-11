using MinimalArchitecture.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Entities.Posts.Models
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Html { get; set; }

        public int Owner { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

    }
}
