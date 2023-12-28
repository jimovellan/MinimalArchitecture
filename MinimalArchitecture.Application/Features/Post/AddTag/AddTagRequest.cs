using MediatR;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Posts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Post.AddTag
{
    public class AddTagRequest: IRequest<Result<Tag>>
    {
        public string Name { get; set; } = default!;
    }
}
