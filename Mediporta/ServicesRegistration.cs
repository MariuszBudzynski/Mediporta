using Microsoft.OpenApi.Models;

namespace Mediporta
{
    public static class ServicesRegistration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
               .AddNegotiate();

            services.AddAuthorization(options =>
            {               
                options.FallbackPolicy = options.DefaultPolicy;
            });

            services.AddDbContext<TagDbContext<Tag>>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("TagDbContextConnection"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mediporta API", Version = "v1" });
            });

            services.AddScoped<AutoDataLoader<Tag>>();
            services.AddScoped<IDataRepository<Tag>, DataRepository<Tag>>();
            services.AddScoped<IFirstLoadDataSaveUseCase<Tag>,FirstLoadDataSaveUseCase<Tag>>();
            services.AddScoped<IGetAllDataUseCase<Tag>, GetAllDataUseCase<Tag>>();
            services.AddScoped<IStatisticsCalculator, StatisticsCalculator<Tag>>();
            services.AddScoped<IForceLoadDataUseCase<Tag>,ForceLoadDataUseCase<Tag>>();
            services.AddScoped<ResponseHandlerService>();
            services.AddScoped<PaginationService<ITagDTO>>();
            services.AddScoped<SortingService<ITagDTO>>();
            services.AddScoped<StatisticsCalculator<Tag>>();

        }
    }
}
