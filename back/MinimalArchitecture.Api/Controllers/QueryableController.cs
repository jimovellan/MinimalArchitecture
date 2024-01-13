using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using MinimalArchitecture.Architecture.Repository;
using MinimalArchitecture.Entities.Repository;
using MinimalArchitecture.Common.Extensions;

namespace MinimalArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueryableController<TEntity> : ControllerBase where TEntity : BaseEntity
    {
        private readonly AppDBContext _ctx;

        public QueryableController(AppDBContext dbContext)
        {
            _ctx = dbContext;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<TEntity> Get()
        {

            return _ctx.Set<TEntity>().AsQueryable();

        }

        [HttpGet("Model")]
        public Object GetModel()
        {
            return typeof(TEntity).GetStructureProperties().ToDictionary(p=>p.Nombre, p=>p.Tipo);
        }

    }
}
