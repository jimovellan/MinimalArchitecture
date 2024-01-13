using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Entities.Repository
{
    public interface ISpecificationResolver
    {
        Task<IEnumerable<T>> Execute<T>(SpecificationPattern<T> spec, CancellationToken cancellation = default) where T : BaseEntity;
    }
}
