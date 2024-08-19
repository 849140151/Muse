using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Win = System.Windows;


namespace Muse.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Win.Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
           .ConfigureServices((hostContext, services) =>
           {
               services.AddSingleton<MainWindow>();

           })
           .Build();
    }

    protected override async void OnStartup(Win.StartupEventArgs e)
    {
        await AppHost!.StartAsync();
        var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
        startupForm.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(Win.ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit( e);
    }

}