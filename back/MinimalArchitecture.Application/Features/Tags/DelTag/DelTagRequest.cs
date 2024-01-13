using MediatR;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Posts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Tags.DelTag
{
    public class DelTagRequest : IRequest<Result>
    {
        public int Id { get; set; }

    }
}
