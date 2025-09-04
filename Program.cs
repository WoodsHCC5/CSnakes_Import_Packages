using CSnakes.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var home = Path.Join(Environment.CurrentDirectory);
        var venv = Path.Join(home, ".venv");

        services
            .WithPython()
            .WithHome(home)
            .WithVirtualEnvironment(venv)
            .FromRedistributable("3.12")
            .WithPipInstaller();
    });

var app = builder.Build();

var env = app.Services.GetRequiredService<IPythonEnvironment>();

var home = Path.Join(Environment.CurrentDirectory);

var advancedDemo = env.Kmeans();
var data = advancedDemo.CalculateKmeansInertia([(100, 2), (200, 3)], 2);
Console.WriteLine($"Inertia: {data.Item2}");
app.Run();