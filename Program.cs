using LineOfficial_MVC.Models;
using Serilog;
using Serilog.Events;

// Serilog.AspNetCore �Ĥ@���q��l�Ƴ]�w
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger(); // �qappsettings.jsonŪ��log�]�w

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Serilog.AspNetCore �ĤG���q��l�Ƴ]�w
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration) // �q�]�w�ɤ�Ū��
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console());

    // Add services to the container.
    builder.Services.AddControllersWithViews();
    //builder.Services.AddControllers();// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    //builder.Services.AddEndpointsApiExplorer();
    // Add configuration setting 
    AppSettings appsettings = new();
    // ���oAppSettings
    builder.Configuration.GetSection("AppSettings").Bind(appsettings);
    // �`�JAppSettings���A��
    builder.Services.AddSingleton(appsettings);

    var app = builder.Build();

    // Serilog.AspNetCore �g�J log file �� MiddleWare �]�w
    app.UseSerilogRequestLogging();

    // Serilog.Extensions.Logging.File �]�w
    //var appPath = Directory.GetCurrentDirectory();
    //var loggerFactory = app.Services.GetService<ILoggerFactory>();
    //loggerFactory.AddFile(Path.Combine(appPath, builder.Configuration["LogFilePath"].ToString()), LogLevel.Error);

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=LineOAuth}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception e)
{
    Log.Error(e, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}