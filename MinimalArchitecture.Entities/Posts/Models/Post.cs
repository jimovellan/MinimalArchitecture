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
        public Post()
        {
            Tags = new List<Tag>();
        }
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Html { get; set; } = default!;

        public int Owner { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

    }
}
