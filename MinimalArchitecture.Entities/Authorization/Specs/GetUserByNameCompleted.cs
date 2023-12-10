using MinimalArchitecture.Entities.Authorization.Models;
using MinimalArchitecture.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Entities.Authorization.Specs
{
    public class GetUserByNameCompleted: SpecificationPattern<User>
    {
        public GetUserByNameCompleted(string userName):base()
        {
            Filter = user => user.Name == userName;

            Includes = new List<Expression<Func<User, object>>>()
            {
                i=>i.Roles
            };

            Caching = false;
        }

        
    }
}
