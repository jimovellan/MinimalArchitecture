using MinimalArchitecture.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _ctx;

        public UnitOfWork(AppDBContext context)
        {
            _ctx = context;
        }

        public async Task Save()
        {
           await _ctx.SaveChangesAsync();
        }
    }
}
