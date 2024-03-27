var builder = WebApplication.CreateBuilder(args);

ServicesRegistration.RegisterServices(builder.Services, builder.Configuration);


var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mediporta API v1");
    });
}

app.UseHttpsRedirection();

Routes.ConfigureRoutes(app);

//var loadData = app.Services.CreateScope().ServiceProvider.GetRequiredService<AutoDataLoader<Tag>>();
//await loadData.LoadDataJSON();

app.Run();