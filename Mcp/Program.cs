using ApiSdk;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

var authProvider = new AnonymousAuthenticationProvider();
var adapter = new HttpClientRequestAdapter(authProvider);
builder.Services.AddSingleton<IRequestAdapter>(adapter);
builder.Services.AddSingleton<HeroClient>();

await builder.Build().RunAsync();


// FOR DEBUGGING
// var host = builder.Build();
// host.Start();
//
// Console.WriteLine("Server started");
//
// host.Services.GetService<HeroApi>();
// Console.WriteLine("HeroApi service registered");
