namespace Mediporta
{
    public static class ServicesRegistration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
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

            services.AddScoped<AutoDataLoader<Tag>>();
            services.AddScoped<IDataRepository<Tag>, DataRepository<Tag>>();
            services.AddScoped<IFirstLoadDataSaveUseCase<Tag>,FirstLoadDataSaveUseCase<Tag>>();
            services.AddScoped<IGetAllDataUseCase<Tag>, GetAllDataUseCase<Tag>>();
            services.AddScoped<IStatisticsCalculator, StatisticsCalculator<Tag>>();
            services.AddScoped<ResponseHandlerService>();
            services.AddScoped<PaginationService<ITagDTO>>();
            services.AddScoped<SortingService<ITagDTO>>();
            services.AddScoped<StatisticsCalculator<Tag>>();

        }
    }
}
