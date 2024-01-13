using MediatR;
using MinimalArchitecture.Entities.Posts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Tags.GetTag
{
    public class GetTagRequest : IRequest<IQueryable<Tag>>
    {
    }
}
