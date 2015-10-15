using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using uwpdevholsService.DataObjects;
using uwpdevholsService.Models;
using Microsoft.Azure.Mobile.Server.Authentication;

namespace uwpdevholsService.Controllers
{
    // Add the [Authorize] attribute to the TodoItemController class. 
    // This requires that all operations against the TodoItem table be made by an authenticated user. 
    // To restrict access only to specific methods, you can also apply this attribute just to those methods instead of the class.
    [Authorize]
    public class TodoItemController : TableController<TodoItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            uwpdevholsContext context = new uwpdevholsContext();
            DomainManager = new EntityDomainManager<TodoItem>(context, Request);
        }

        // GET tables/TodoItem
        public IQueryable<TodoItem> GetAllTodoItems()
        {
            // Get the logged in user
            var currentUser = User as MobileAppUser;

            return Query().Where(todo => todo.UserId == currentUser.Id);
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<TodoItem> GetTodoItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TodoItem> PatchTodoItem(string id, Delta<TodoItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostTodoItem(TodoItem item)
        {
            // Get the logged in user
            var currentUser = User as MobileAppUser;

            // Set the user ID on the item
            item.UserId = currentUser.Id;

            TodoItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}