namespace Mediporta
{
    public class Routes
    {
        public static void ConfigureRoutes(WebApplication app)
        {
            app.MapGet("/tags", async (
                ResponseHandlerService responseHandlerService,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 10,
                [FromQuery] string sortBy = "name",
                [FromQuery] string sortOrder = "asc") =>
            {
                return await responseHandlerService.ReturnResponse(page, pageSize, sortBy, sortOrder);
            });
        }
    }
}
