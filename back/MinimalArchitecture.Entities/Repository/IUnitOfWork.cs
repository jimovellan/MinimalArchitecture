using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Entities.Repository
{
    public interface IUnitOfWork
    {
        public Task Save();
    }
}
