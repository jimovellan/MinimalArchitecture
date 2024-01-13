using MinimalArchitecture.Entities.Authorization.Models;
using MinimalArchitecture.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Entities.Authorization.Specs
{
    public class GetUserByIdCompleted:SpecificationPattern<User>
    {
        public GetUserByIdCompleted(int id, bool active = true, bool track = true) : base()
        {
            Filter = user => user.Id == id && user.Active == active;

            Includes = new List<Expression<Func<User, object>>>()
            {
                i=>i.Roles,
                i =>i.Tokens
            };

            Caching = track;
        }


    }
}
