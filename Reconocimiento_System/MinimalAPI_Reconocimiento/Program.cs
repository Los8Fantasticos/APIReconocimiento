using Microsoft.EntityFrameworkCore;

using MinimalAPI_Reconocimiento.Configurations;
using MinimalAPI_Reconocimiento.Contracts.Repositories;
using MinimalAPI_Reconocimiento.Contracts.Services;
using MinimalAPI_Reconocimiento.Endpoints.Patente;
using MinimalAPI_Reconocimiento.Infrastructure;
using MinimalAPI_Reconocimiento.Infrastructure.Repositories;
using MinimalAPI_Reconocimiento.Infrastructure.Storage;
using MinimalAPI_Reconocimiento.Receiver;
using MinimalAPI_Reconocimiento.Services;
using RabbitMqService.Queues;
using RabbitMqService.RabbitMq;
using Serilog;

var builder = WebApplication
                .CreateBuilder(args)
                .ConfigureBuilder();

ConfigureServices(builder.Services, builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsApi",
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:44394")
                                 .AllowAnyMethod()
                                 .AllowAnyHeader()
                                 .AllowCredentials(); ;
                      });

});
var app = builder.Build();

app.UseCors("CorsApi");
using (var scope = app.Services.CreateScope())
{
    var databaseContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
    if (databaseContext != null)
    {
        //databaseContext.Database.EnsureCreated();
    }
    scope.ServiceProvider.GetService<PatenteService>();
    scope.ServiceProvider.GetService<PatenteEndpoint>()?.MapPatenteEndpoints(app);
}

Configure(app, app.Environment);
app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.ConfigureLogger(builder);
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
    services.AddScoped<PatenteEndpoint>();
    services.AddScoped<IPatenteRepository, PatenteRepository>();
    services.AddScoped<IPatenteService, PatenteService>();

    var connectionString = builder.Configuration.GetConnectionString("SqlConnection") ?? builder.Configuration["ConnectionStrings"]?.ToString() ?? "";

    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, options =>
    {
        options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
    }), ServiceLifetime.Singleton);

    services.AddRabbitMq(settings =>
    {
        settings.ConnectionString = configuration.GetValue<string>("RabbitMq:ConnectionString");
        settings.ExchangeName = configuration.GetValue<string>("AppSettings:ApplicationName");
        settings.QueuePrefetchCount = configuration.GetValue<ushort>("AppSettings:QueuePrefetchCount");
    }, queues =>
    {
        queues.Add<Reconocimiento>();
        queues.Add<Multas>();
        queues.Add<Pagos>();
    })
    .AddReceiver<PatenteReceiver<string>, string, PatenteService>();

    //builder.Services.AddConfig<ApiReconocimientoConfig>(builder.Configuration, nameof(ApiReconocimientoConfig));
}


void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        var context = app.ApplicationServices.GetService<ApplicationDbContext>();
        context?.Database?.Migrate();
        context?.AddPatente(randomBoolean: true, count: 50);
    }

    app.UseHttpsRedirection();

    app.UseRouting();
}