using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    public static class ControllerExtensions
    {
        public static ActionResult<T> Maybe<T>(this Controller controller, T entity)
        {
            if (entity == null)
            {
                return new NotFoundResult();
            }
            else
            {
                return new OkObjectResult(entity);
            }
        }
    }
}
