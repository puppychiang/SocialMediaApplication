using LineOfficial_MVC.Models;
using Serilog;
using Serilog.Events;

// Serilog.AspNetCore 第一階段初始化設定
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger(); // 從appsettings.json讀取log設定

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Serilog.AspNetCore 第二階段初始化設定
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration) // 從設定檔中讀取
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console());

    // Add services to the container.
    builder.Services.AddControllersWithViews();
    //builder.Services.AddControllers();// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    //builder.Services.AddEndpointsApiExplorer();
    // Add configuration setting 
    AppSettings appsettings = new();
    // 取得AppSettings
    builder.Configuration.GetSection("AppSettings").Bind(appsettings);
    // 注入AppSettings的服務
    builder.Services.AddSingleton(appsettings);

    var app = builder.Build();

    // Serilog.AspNetCore 寫入 log file 的 MiddleWare 設定
    app.UseSerilogRequestLogging();

    // Serilog.Extensions.Logging.File 設定
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