var builder = WebApplication.CreateBuilder(args);

ServicesRegistration.RegisterServices(builder.Services, builder.Configuration);


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