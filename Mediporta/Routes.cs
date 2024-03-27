using Mediporta.Data.AutoDataLoader;

namespace Mediporta
{
    public class Routes
    {
        public static void ConfigureRoutes(WebApplication app)
        {
            app.MapGet("/tags", async (
                ResponseHandlerService responseHandlerService, int page = 1,int pageSize = 10,string sortBy = "name",string sortOrder = "asc") =>
            {
                return await responseHandlerService.ReturnResponse(page, pageSize, sortBy, sortOrder);
            }
            );


            app.MapPost("/tags/force-reload", async (ResponseHandlerService responseHandlerService) =>
            {
                return await responseHandlerService.ReturnResponse();
            });
        }
    }
}
