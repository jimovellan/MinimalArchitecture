using MinimalArchitecture.Architecture.Repository;
using MinimalArchitecture.Entities.Posts.Models;

namespace MinimalArchitecture.Api.Controllers
{
    public class CategoryController:QueryableController<Category>
    {
        public CategoryController(AppDBContext appDBContext):base(appDBContext)
        {
            
        }
    }
}
