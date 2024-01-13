using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Entities.Repository
{
     public abstract class SpecificationPattern<T> where T : BaseEntity
     {
        public List<Expression<Func<T, object>>> Includes { get; internal set; }
        public Expression<Func<T, bool>> Filter { get; internal set; }

        public bool Caching { get; internal set; }
        private Func<T,bool> computedFilter => (Filter is not null) ? Filter.Compile() : null;

        public SpecificationPattern()
        {
            
        }

        public  bool CheckCondition(T entity)
        {
            return computedFilter(entity);
        }
    }
}
