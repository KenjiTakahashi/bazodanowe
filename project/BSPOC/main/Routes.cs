using System.Web.Routing;
using RestfulRouting;
using main.Controllers;

[assembly: WebActivator.PreApplicationStartMethod(typeof(main.Routes), "Start")]

namespace main
{
    public class Routes : RouteSet
    {
        public override void Map(IMapper map)
        {
            map.DebugRoute("routedebug");

            map.Root<HomeController>(x => x.Index());

            map.Resources<UsersController>(users =>
                users.Resources<ShelvesController>()
            );
            map.Resources<ShelvesController>(shelves => {
                shelves.Resources<BooksController>();
                shelves.Resources<UsersController>();
            });
            map.Resources<BooksController>(books => {
                books.Resources<AuthorsController>();
                books.Resources<ShelvesController>();
            });
            map.Resources<AuthorsController>(authors =>
                authors.Resources<BooksController>()
            );
        }

        public static void Start()
        {
            var routes = RouteTable.Routes;
            routes.MapRoutes<Routes>();
        }
    }
}