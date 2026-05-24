using API.RequestHelper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
         protected ActionResult CreatePagedResult<T>(IGenericRepository<T> repo, ISpecification<T> spec
         , int pageIndex, int pageSize) where T : BaseEntity
        {
          
            var count = repo.CountAsync(spec).Result;
            var data = repo.ListAsync(spec).Result;
            var pagination = new Pagination<T>(pageIndex, pageSize, count, data);
            return Ok(pagination);

        }
}


}