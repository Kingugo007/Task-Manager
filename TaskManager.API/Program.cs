using Serilog;
using TaskManager.API.Extensions;
using TaskManager.Core;
using TaskManager.Core.Services;




try
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var isDevelopment = environment == Environments.Development;

    IConfiguration config = ConfigurationSetup.GetConfig(isDevelopment);
    LogSettings.SetUpSerilog(config);
    Log.Logger.Information("Application starting up");
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;

    // Add services to the container.
   
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpClient<IHttpCommandHandler, HttpCommandHandler>();
    builder.Services.AddScoped<IHttpCommandHandler, HttpCommandHandler>();
    builder.Services.AddAutoMapper(typeof(MapperInitializer));

    builder.Services.AddScoped<ITaskServices, TaskServices>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    // NLog: catch setup errors
    Log.Logger.Fatal(e.Message, "The application failed to start correctly");
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    Log.CloseAndFlush();
}