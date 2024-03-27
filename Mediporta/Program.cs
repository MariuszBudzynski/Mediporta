var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

string logsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");

if (!Directory.Exists(logsDirectory))
{
    Directory.CreateDirectory(logsDirectory);
}


ServicesRegistration.RegisterServices(builder.Services, builder.Configuration);
var app = builder.Build();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mediporta API v1");
    });
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

Routes.ConfigureRoutes(app);

var loadData = app.Services.CreateScope().ServiceProvider.GetRequiredService<AutoDataLoader<Tag>>();
await loadData.LoadDataJSON();

app.Run();