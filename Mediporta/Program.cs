var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

ServicesRegistration.RegisterServices(builder.Services, configuration);


var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

Routes.ConfigureRoutes(app);

var loadData = app.Services.CreateScope().ServiceProvider.GetRequiredService<AutoDataLoader<Tag>>();
await loadData.LoadDataJSON();

app.Run();