using MediatR;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Posts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Tags.UpdTag
{
    public class UpdTagRequest : IRequest<Result<Tag>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
